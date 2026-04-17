using appointmentapi.Models.AuthEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AuthMaps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.CreationDate)
            .IsRequired();

        builder.Property(u => u.Active)
            .IsRequired();

        builder.HasOne(u => u.Person)
            .WithMany()
            .HasForeignKey(u => u.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
