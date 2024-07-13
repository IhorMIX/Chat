using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Chat.DAL.Repository;

public class ChatRoomRepository(ChatDbContext chatDbContext) : IChatRoomRepository
{
    public IQueryable<ChatRoom> GetAll()
    {
        return chatDbContext.ChatRooms.AsQueryable();
    }

    public Task<ChatRoom?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return chatDbContext.ChatRooms
            .Include(cr => cr.Users)
            .SingleOrDefaultAsync(cr => cr.Id == id, cancellationToken);
    }

    public async Task<ChatRoom> CreateAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default)
    {
        await chatDbContext.ChatRooms.AddAsync(chatRoom, cancellationToken);
        await chatDbContext.SaveChangesAsync(cancellationToken);
        return chatRoom;
    }

    public async Task DeleteAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default)
    {
        chatDbContext.ChatRooms.Remove(chatRoom);
        await chatDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ChatRoom> UpdateAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default)
    {
        chatDbContext.ChatRooms.Update(chatRoom);
        await chatDbContext.SaveChangesAsync(cancellationToken);
        return chatRoom;
    }
}