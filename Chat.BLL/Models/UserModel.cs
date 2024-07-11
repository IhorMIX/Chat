namespace Chat.BLL.Models;

public class UserModel : BaseModel
{
    public string Name { get; set; } = null!;
    
    public IEnumerable<ChatRoomModel> JoinedChatRooms { get; set; } = null!;
    public IEnumerable<ChatRoomModel> CreatedChatRooms { get; set; } = null!;
    public IEnumerable<MessageModel> Messages { get; set; } = null!;
}