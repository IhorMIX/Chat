using Chat.BLL.Models;

namespace Chat.BLL.Services.Interface;

public interface IChatRoomService : IBasicService<ChatRoomModel>
{
    Task<ChatRoomModel> CreateChatRoomAsync(int userId, string roomName, CancellationToken cancellationToken = default);
    Task<ChatRoomModel> UpdateChatRoomAsync(int userId, int roomId, ChatRoomModel chatRoomModel, CancellationToken cancellationToken = default);
    Task DeleteChatRoomAsync(int userId, int roomId, CancellationToken cancellationToken = default);
    Task JoinChatRoomAsync(int userId, int roomId, CancellationToken cancellationToken = default);
    Task LeaveChatRoomAsync(int userId, int roomId, CancellationToken cancellationToken = default);
}