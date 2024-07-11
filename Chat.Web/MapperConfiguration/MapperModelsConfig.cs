using AutoMapper;
using Chat.BLL.Models;
using Chat.DAL.Entity;

namespace Chat.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
{
    public MapperModelsConfig()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<Message, MessageModel>().ReverseMap();
        CreateMap<ChatRoom, ChatRoomModel>().ReverseMap();
    }
}