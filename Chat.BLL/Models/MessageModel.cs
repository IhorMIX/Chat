namespace Chat.BLL.Models;

public class MessageModel : BaseModel
{
    public ChatRoomModel ChatRoom { get; set; } = null!;
    public int ChatRoomId { get; set; }
    
    public UserModel User { get; set; } = null!;
    public int UserId { get; set; }

    public string Text { get; set; } = null!;
}