using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Chat.DAL.Repository;

public class MessageRepository(ChatDbContext chatDbContext) : IMessageRepository
{
    public IQueryable<Message> GetAll()
    {
        return chatDbContext.Messages.AsQueryable();
    }

    public async Task<Message?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await chatDbContext.Messages.Include(r => r.User).Include(r => r.ChatRoom)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default)
    {
        await chatDbContext.Messages.AddAsync(message, cancellationToken);
        await chatDbContext.SaveChangesAsync(cancellationToken);
        return message;
    }

    public async Task DeleteAsync(Message message, CancellationToken cancellationToken = default)
    {
        chatDbContext.Messages.Remove(message);
        await chatDbContext.SaveChangesAsync(cancellationToken);
    }
}