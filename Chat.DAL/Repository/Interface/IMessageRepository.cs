using Chat.DAL.Entity;

namespace Chat.DAL.Repository.Interface;

public interface IMessageRepository: IBasicRepository<Message>
{
    public Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(Message message, CancellationToken cancellationToken = default);
}