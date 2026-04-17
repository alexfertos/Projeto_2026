using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Models.AuthEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AuthMaps;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.CPF)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasOne(u => u.Phone)
          .WithOne()//fk
          .HasForeignKey<Person>(u => u.PhoneId)
          .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.BirthDate);
    }

}