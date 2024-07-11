using Chat.DAL.Entity;

namespace Chat.DAL.Repository.Interface;

public interface IUserRepository : IBasicRepository<User>
{
    public Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    public Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}