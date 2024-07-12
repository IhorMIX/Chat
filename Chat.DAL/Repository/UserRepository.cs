using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Chat.DAL.Repository;

public class UserRepository(ChatDbContext chatDbContext) : IUserRepository
{
    public IQueryable<User> GetAll()
    {
        return chatDbContext.Users.Include(r => r.CreatedChatRooms).Include(r => r.JoinedChatRooms).AsQueryable();
    }

    public Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return chatDbContext.Users.Include(r => r.CreatedChatRooms).Include(r => r.JoinedChatRooms)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await chatDbContext.Users.AddAsync(user, cancellationToken);
        await chatDbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        chatDbContext.Users.Remove(user);
        await chatDbContext.SaveChangesAsync(cancellationToken);
    }
}