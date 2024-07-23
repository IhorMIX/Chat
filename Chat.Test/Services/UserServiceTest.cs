using Chat.BLL.Exceptions;
using Chat.BLL.Services;
using Chat.BLL.Services.Interface;
using Chat.DAL.Repository;
using Chat.DAL.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Chat.Test.Services;

public class UserServiceTest : DefaultServiceTest<IUserService, UserService>
{
    protected override void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        base.SetUpAdditionalDependencies(services);
    }
    
    [Test]
    public async Task CreateUser_WithCorrectData_Success()
    {
        var user = await Service.CreateUserAsync("User");
        Assert.That(user.Name, Is.EqualTo("User"));
    }
    
    [Test]
    public async Task DeleteUser_WithCorrectData_Success()
    {
        var user = await Service.CreateUserAsync("User");
        Assert.That(user.Name, Is.EqualTo("User"));
        
        await Service.DeleteUserAsync(user.Id);
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await Service.DeleteUserAsync(user.Id));
    }
}