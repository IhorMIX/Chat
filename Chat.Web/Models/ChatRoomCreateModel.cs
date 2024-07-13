namespace Chat.Web.Models;

public class ChatRoomCreateModel
{
    public string Name { get; set; } = null!;
    public int CreatorId { get; set; }
}