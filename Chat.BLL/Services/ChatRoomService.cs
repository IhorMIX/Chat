using AutoMapper;
using Chat.BLL.Exceptions;
using Chat.BLL.Helpers;
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
        
        var chatRoomDb = await chatRoomRepository.CreateAsync(new ChatRoom() { Name = chatRoomName, CreatorId = userId, Users = new List<User>{userDb}}, cancellationToken);
        var chatRoomModel = mapper.Map<ChatRoomModel>(chatRoomDb);
        return chatRoomModel;
    }

    public async Task<ChatRoomModel> UpdateChatRoomAsync(int userId, int chatRoomId, ChatRoomModel chatRoomModel, CancellationToken cancellationToken = default)
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
        {
            throw new NotPermissionException("There are not permission to do the operation");
        }
        foreach (var (chatProperty, chatDbProperty) in ReflectionHelper.WidgetUtil<ChatRoomModel, ChatRoom>.PropertyMap)
        {
            var roomSourceValue = chatProperty.GetValue(chatRoomModel);
            var roomTargetValue = chatDbProperty.GetValue(chatRoomDb);

            if (roomSourceValue != null && 
                !ReferenceEquals(roomSourceValue, "") &&
                !Equals(roomSourceValue, 0) &&
                !roomSourceValue.Equals(roomTargetValue))
            {
                chatDbProperty.SetValue(chatRoomDb, roomSourceValue);
            }
        }
        
        var chatModel = await chatRoomRepository.UpdateAsync(chatRoomDb, cancellationToken);
        return mapper.Map<ChatRoomModel>(chatModel);
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
        {
            throw new NotPermissionException("There are not permission to do the operation");
        }

        await chatRoomRepository.DeleteAsync(chatRoomDb, cancellationToken);
    }

    public async Task JoinChatRoomAsync(int userId, int chatRoomId, CancellationToken cancellationToken = default)
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
        
        chatRoomDb.Users.Add(userDb);
        await chatRoomRepository.UpdateAsync(chatRoomDb, cancellationToken);
    }

    public async Task LeaveChatRoomAsync(int userId, int chatRoomId, CancellationToken cancellationToken = default)
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

        if (chatRoomDb.CreatorId == userId)
        {
            throw new NotPermissionException("There are not permission to do the operation");
        }

        chatRoomDb.Users.Remove(userDb);
        await chatRoomRepository.UpdateAsync(chatRoomDb, cancellationToken);
    }
    
}

