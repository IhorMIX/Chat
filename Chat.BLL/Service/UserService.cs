using AutoMapper;
using Chat.BLL.Exception;
using Chat.BLL.Models;
using Chat.BLL.Service.Interface;
using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;

namespace Chat.BLL.Service;

public class UserService(IUserRepository userRepository,  IMapper mapper) : IUserService
{
    public async Task<UserModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var userDb = await userRepository.GetByIdAsync(id, cancellationToken);
        if (userDb == null)
        {
            throw new UserNotFoundException($"User with this Id {id} not found");
        }
        var userModel = mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task<UserModel> CreateUserAsync(string name, CancellationToken cancellationToken = default)
    {
        var userDb = await userRepository.CreateAsync(new User { Name = name }, cancellationToken);
        var userModel = mapper.Map<UserModel>(userDb);
        return userModel;
    }

    public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (userDb is not null)
        {
            await userRepository.DeleteAsync(userDb,cancellationToken);
        }
        throw new UserNotFoundException($"User with this Id {userId} not found");
    }
}