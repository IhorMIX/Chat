using Chat.BLL.Services.Interface;
using Chat.BLL.Services;
using Microsoft.EntityFrameworkCore;
using Chat.DAL;
using Chat.DAL.Repository;
using Chat.DAL.Repository.Interface;
using Chat.Web.Hubs;
using Microsoft.AspNetCore.Http.Connections;
using Newtonsoft.Json.Converters;

namespace Chat.Web;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR().AddHubOptions<ChatRoomHub>(options =>
        {
            options.EnableDetailedErrors = true;
        });
        
        services.AddAutoMapper(typeof(Startup));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IChatRoomService, ChatRoomService>();
        
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");

        services.AddDbContext<ChatDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddControllers().AddNewtonsoftJson(opt => 
            opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseRouting();
        
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatRoomHub>("/chatRoomHub", options =>
            {
                options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
            });
        });
    }
}