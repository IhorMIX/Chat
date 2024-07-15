using AutoMapper;
using Chat.BLL.Exceptions;
using Chat.BLL.Models;
using Chat.BLL.Services.Interface;
using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;

namespace Chat.BLL.Services;

public class MessageService(IMessageRepository messageRepository,IUserRepository userRepository, IChatRoomRepository chatRoomRepository,  IMapper mapper) : IMessageService
{
    public async Task<MessageModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var messageModel = await messageRepository.GetByIdAsync(id, cancellationToken);
        return mapper.Map<MessageModel>(messageModel);
    }

    public async Task<MessageModel> CreateMessageAsync(int userId, int chatRoomId, string text, CancellationToken cancellationToken = default)
    {
        var userDb = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb == null)
        {
            throw new UserNotFoundException($"User with Id {userId} not found");
        }
        
        var chatRoomDb = await chatRoomRepository.GetByIdAsync(chatRoomId, cancellationToken);
        if (chatRoomDb == null)
        {
            throw new ChatRoomNotFoundException($"ChatRoom with Id {chatRoomId} not found");
        }

        var messageModel = await messageRepository.CreateAsync(new Message
        {
            UserId = userId,
            ChatRoomId = chatRoomId,
            Text = text
        }, cancellationToken);

        return mapper.Map<MessageModel>(messageModel);
    }
}