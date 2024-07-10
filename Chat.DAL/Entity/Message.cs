namespace Chat.DAL.Entity;

public class Message : BaseEntity
{
    public ChatRoom ChatRoom { get; set; } = null!;
    public int ChatRoomId { get; set; }
    
    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public string Text { get; set; } = null!;
}