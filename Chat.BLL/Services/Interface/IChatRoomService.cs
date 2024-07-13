using Chat.BLL.Models;

namespace Chat.BLL.Services.Interface;

public interface IChatRoomService : IBasicService<ChatRoomModel>
{
    Task<ChatRoomModel> CreateChatRoomAsync(int userId, string chatRoomName, CancellationToken cancellationToken = default);
    Task<ChatRoomModel> UpdateChatRoomAsync(int userId, int chatRoomId, ChatRoomModel chatRoomModel, CancellationToken cancellationToken = default);
    Task DeleteChatRoomAsync(int userId, int chatRoomId, CancellationToken cancellationToken = default);
    Task JoinChatRoomAsync(int userId, int chatRoomId, CancellationToken cancellationToken = default);
    Task LeaveChatRoomAsync(int userId, int chatRoomId, CancellationToken cancellationToken = default);
}