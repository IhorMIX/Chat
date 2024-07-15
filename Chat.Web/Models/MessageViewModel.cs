using Chat.DAL.Entity;

namespace Chat.Web.Models;

public class MessageViewModel
{
    public ChatRoom ChatRoom { get; set; } = null!;
    public int ChatRoomId { get; set; }
    
    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public string Text { get; set; } = null!;
}