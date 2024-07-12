using AutoMapper;
using Chat.BLL.Exceptions;
using Chat.BLL.Models;
using Chat.BLL.Services.Interface;
using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;

namespace Chat.BLL.Services;

public class ChatRoomService(IChatRoomRepository chatRoomRepository,IUserRepository userRepository, IMapper mapper) : IChatRoomService
{
    public async Task<ChatRoomModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var chatRoomDb = await chatRoomRepository.GetByIdAsync(id, cancellationToken);
        if (chatRoomDb == null)
        {
            throw new UserNotFoundException($"ChatRoom with this Id {id} not found");
        }
        var chatRoomModel = mapper.Map<ChatRoomModel>(chatRoomDb);
        return chatRoomModel;
    }

    public async Task<ChatRoomModel> CreateChatRoomAsync(int userId, string chatRoomName, CancellationToken cancellationToken = default)
    {
        var userDb = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb == null)
        {
            throw new UserNotFoundException($"User with Id {userId} not found");
        }
        
        var chatRoomDb = await chatRoomRepository.CreateAsync(new ChatRoom() { Name = chatRoomName, CreatorId = userId}, cancellationToken);
        var chatRoomModel = mapper.Map<ChatRoomModel>(chatRoomDb);
        return chatRoomModel;
    }

    public Task<ChatRoomModel> UpdateChatRoomAsync(int userId, int roomId, ChatRoomModel chatRoomModel,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteChatRoomAsync(int userId, int chatRoomId, CancellationToken cancellationToken = default)
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
        
        if (chatRoomDb.CreatorId != userDb.Id)
            throw new NotPermissionException("There are not permission to do the operation");

        await chatRoomRepository.DeleteAsync(chatRoomDb, cancellationToken);
    }

    public Task JoinChatRoomAsync(int userId, int roomId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task LeaveChatRoomAsync(int userId, int roomId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
}
