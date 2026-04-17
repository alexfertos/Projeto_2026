using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps;

public class SpecialtyMap : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.ToTable("Specialty");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(255);

        builder.HasMany(s => s.UnitSpecialties)
            .WithOne(us => us.Specialty)
            .HasForeignKey(us => us.SpecialtyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

