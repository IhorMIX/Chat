using System.Text.Json;
using AutoMapper;
using Chat.BLL.Exceptions;
using Chat.BLL.Services.Interface;
using Chat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Web.Hubs;

[AllowAnonymous]
public class ChatRoomHub(IUserService userService, IChatRoomService roomService, IMessageService messageService, IMapper mapper) : Hub
{
    public async Task SendMessage(int userId, int chatRoomId, string textMess)
    {
        var messageModel = await messageService.CreateMessageAsync(userId, chatRoomId, textMess);
        var messageViewModel = mapper.Map<MessageViewModel>(messageModel);

        await Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", 
            JsonSerializer.Serialize(messageViewModel));
    }
    
    public override async Task OnConnectedAsync()
    {
        try
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext == null)
            {
                throw new ArgumentException("HTTP context is null");
            }

            var chatRoomIdValues = httpContext.Request.Query["chatRoomId"];
            var userIdValues = httpContext.Request.Query["userId"];

            if (string.IsNullOrEmpty(chatRoomIdValues) || string.IsNullOrEmpty(userIdValues))
            {
                throw new ArgumentException("ChatRoomId or UserId is missing");
            }

            if (int.TryParse(chatRoomIdValues[0], out int chatRoomId) && int.TryParse(userIdValues[0], out int userId))
            {
                var chatRoom = await roomService.GetByIdAsync(chatRoomId);
                if (chatRoom == null)
                {
                    throw new ChatRoomNotFoundException($"ChatRoom with Id {chatRoomId} not found");
                }

                await roomService.JoinChatRoomAsync(userId, chatRoomId);
                await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
            }
            else
            {
                throw new ArgumentException("Invalid ChatRoomId or UserId");
            }

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext == null)
        {
            throw new ArgumentException("HTTP context is null");
        }

        var chatRoomIdValues = httpContext.Request.Query["chatRoomId"];
        var userIdValues = httpContext.Request.Query["userId"];

        if (!string.IsNullOrEmpty(chatRoomIdValues) && 
            int.TryParse(chatRoomIdValues[0], out int chatRoomId) &&
            int.TryParse(userIdValues[0], out int userId))
        {
            var chatRoom = await roomService.GetByIdAsync(chatRoomId);
            if (chatRoom == null)
            {
                throw new ChatRoomNotFoundException($"ChatRoom with Id {chatRoomId} not found");
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        await base.OnDisconnectedAsync(exception);
    }
}