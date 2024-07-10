using Chat.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired();

        builder.HasMany(u => u.JoinedChatRooms)
            .WithMany(c => c.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserChatRoom",
                j => j.HasOne<ChatRoom>()
                    .WithMany()
                    .HasForeignKey("ChatRoomId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Restrict)
            );

        builder.HasMany(u => u.CreatedChatRooms)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Messages)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}