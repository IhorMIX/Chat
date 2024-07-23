namespace Chat.DAL.Entity;

public class ChatRoom : BaseEntity
{
    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;

    public string Name { get; set; } = null!;

    public IEnumerable<Message> Messages { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
}