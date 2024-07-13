using AutoMapper;
using Chat.BLL.Models;
using Chat.DAL.Entity;
using Chat.Web.Models;

namespace Chat.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
{
    public MapperModelsConfig()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<UserModel, UserViewModel>();
        
        CreateMap<Message, MessageModel>().ReverseMap();
        
        CreateMap<ChatRoom, ChatRoomModel>().ReverseMap();
        CreateMap<ChatRoomModel, ChatRoomViewModel>().ReverseMap();
    }
}