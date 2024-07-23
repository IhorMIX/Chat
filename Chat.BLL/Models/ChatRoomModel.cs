namespace Chat.BLL.Models;

public class ChatRoomModel : BaseModel
{
    public int CreatorId { get; set; }
    
    public UserModel Creator { get; set; } = null!;
    public string Name { get; set; } = null!;

    public IEnumerable<MessageModel> Messages { get; set; } = null!;
    public ICollection<UserModel> Users { get; set; } = null!;
}