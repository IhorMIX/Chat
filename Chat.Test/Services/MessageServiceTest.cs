using Chat.BLL.Services;
using Chat.BLL.Services.Interface;
using Chat.DAL.Repository;
using Chat.DAL.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Chat.Test.Services;

public class MessageServiceTest : DefaultServiceTest<IMessageService, MessageService>
{
    protected override void SetUpAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IChatRoomService, ChatRoomService>();

        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();
        base.SetUpAdditionalDependencies(services);
    }
    
    [Test]
    public async Task CreateMessage_Success()
    {
        var userService = ServiceProvider.GetRequiredService<IUserService>();
        var roomService = ServiceProvider.GetRequiredService<IChatRoomService>();
        
        var user = await userService.CreateUserAsync("Hurry");
        var chatRoom = await roomService.CreateChatRoomAsync(user.Id, "ChatRoom" );
        Assert.That(chatRoom.Creator.Name, Is.EqualTo(user.Name));
        
        var message1 = await Service.CreateMessageAsync(user.Id, chatRoom.Id, "test user mess1");
        var messages = await Service.GetMessagesAsync(user.Id, chatRoom.Id);
        
        Assert.That(messages.Select(r => r.Text).Contains(message1.Text));
    }
}