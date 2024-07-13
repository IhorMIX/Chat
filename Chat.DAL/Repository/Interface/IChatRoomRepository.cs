using Chat.DAL.Entity;

namespace Chat.DAL.Repository.Interface;

public interface IChatRoomRepository : IBasicRepository<ChatRoom>
{
    public Task<ChatRoom> CreateAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);
    public Task DeleteAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);
    public Task<ChatRoom> UpdateAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);
}