using Chat.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DAL.Configuration;

public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.CreatedChatRooms)
            .HasForeignKey(c => c.CreatorId);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.ChatRoom)
            .HasForeignKey(m => m.ChatRoomId);
            
        builder.HasMany(c => c.Users)
            .WithMany(u => u.JoinedChatRooms)
            .UsingEntity<Dictionary<string, object>>(
                "UserChatRoom",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<ChatRoom>().WithMany().HasForeignKey("ChatRoomId")
            );
    }
}