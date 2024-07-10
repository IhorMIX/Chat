namespace Chat.DAL.Entity;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public IEnumerable<ChatRoom> JoinedChatRooms { get; set; } = null!;
    public IEnumerable<ChatRoom> CreatedChatRooms { get; set; } = null!;
    public IEnumerable<Message> Messages { get; set; } = null!;
}