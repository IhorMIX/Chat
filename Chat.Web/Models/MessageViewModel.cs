using Chat.DAL.Entity;

namespace Chat.Web.Models;

public class MessageViewModel
{
    public int ChatRoomId { get; set; }
    
    public int UserId { get; set; }

    public string Text { get; set; } = null!;
}