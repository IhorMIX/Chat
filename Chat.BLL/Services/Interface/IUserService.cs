using Chat.BLL.Models;

namespace Chat.BLL.Services.Interface;

public interface IUserService : IBasicService<UserModel>
{
    Task<UserModel> CreateUserAsync(string name, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(int userId, CancellationToken cancellationToken = default);
}