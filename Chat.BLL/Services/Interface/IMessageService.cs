using Chat.BLL.Models;

namespace Chat.BLL.Services.Interface;

public interface IMessageService : IBasicService<MessageModel>
{
    Task<MessageModel> CreateMessageAsync(int userId, int chatRoomId, string text, CancellationToken cancellationToken = default);
}