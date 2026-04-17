using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps;

public class UnitSpecialtyMap : IEntityTypeConfiguration<UnitSpecialty>
{
    public void Configure(EntityTypeBuilder<UnitSpecialty> builder)
    {
        builder.ToTable("UnitSpecialty");

        builder.HasKey(us => us.Id);

        builder.HasOne(us => us.Unit)
            .WithMany(u => u.UnitSpecialties)
            .HasForeignKey(us => us.UnitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.Specialty)
            .WithMany(s => s.UnitSpecialties)
            .HasForeignKey(us => us.SpecialtyId)
            .OnDelete(DeleteBehavior.Cascade);

    
    }
}