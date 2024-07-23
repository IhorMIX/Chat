using Chat.BLL.Models;
using Chat.BLL.Services;
using Chat.BLL.Services.Interface;
using Chat.DAL.Repository;
using Chat.DAL.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Chat.Test.Services;

public class СhatRoomServiceTest : DefaultServiceTest<IChatRoomService, ChatRoomService>
{
    protected override void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IChatRoomService, ChatRoomService>();
        base.SetUpAdditionalDependencies(services);
    }

    [Test]
    public async Task CreateChatRoom_WithCorrectData_Success()
    {
        var userService = ServiceProvider.GetRequiredService<IUserService>();
        var user = await userService.CreateUserAsync("User");

        var chatRoom = await Service.CreateChatRoomAsync(user.Id, "TestRoom");
        Assert.That(chatRoom.CreatorId == user.Id);
    }
    
    [Test]
    public async Task CreateChatRoom_DeleteChatRoom_Success()
    {
        var userService = ServiceProvider.GetRequiredService<IUserService>();
        var user = await userService.CreateUserAsync("User");
        var chatRoom = await Service.CreateChatRoomAsync(user.Id, "TestRoom");

        var user2 = await userService.CreateUserAsync("User2");
        var user3 = await userService.CreateUserAsync("User3");
        await Service.JoinChatRoomAsync(user2.Id, chatRoom.Id);
        await Service.JoinChatRoomAsync(user3.Id, chatRoom.Id);
        
        var chatRoomUpdated = await Service.GetByIdAsync(chatRoom.Id);//we receive current data about chatRoom
        
        Assert.That(chatRoomUpdated!.Users.Count(), Is.EqualTo(3));

        await Service.LeaveChatRoomAsync(user3.Id, chatRoom.Id);
        
        chatRoomUpdated = await Service.GetByIdAsync(chatRoom.Id);//we receive current data about chatRoom
        Assert.That(chatRoomUpdated!.Users.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public async Task UpdateRoom_Success()
    {
        const string newName = "newName";
        
        var userService = ServiceProvider.GetRequiredService<IUserService>();
        var user = await userService.CreateUserAsync("Hurry");
        var chatRoom = await Service.CreateChatRoomAsync(user.Id, "ChatRoom" );
        Assert.That(chatRoom.Creator.Name, Is.EqualTo(user.Name));
        
        await Service.UpdateChatRoomAsync(user.Id, chatRoom.Id, new ChatRoomModel()
        {
            Name = "newName",
        });

        var chatRoomUpdated = await Service.GetByIdAsync(chatRoom.Id);
        Assert.That(chatRoomUpdated!.Name, Is.EqualTo(newName));
    }
}