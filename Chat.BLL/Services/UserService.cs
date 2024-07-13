using AutoMapper;
using Chat.BLL.Exceptions;
using Chat.BLL.Models;
using Chat.BLL.Services.Interface;
using Chat.DAL.Entity;
using Chat.DAL.Repository.Interface;

namespace Chat.BLL.Services;

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
        if (userDb is null)
        {
            throw new UserNotFoundException($"User with this Id {userId} not found");
        }
        await userRepository.DeleteAsync(userDb,cancellationToken);
    }
}