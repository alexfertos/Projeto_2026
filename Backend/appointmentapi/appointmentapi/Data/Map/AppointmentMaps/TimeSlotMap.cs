using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps;

public class TimeSlotMap : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.ToTable("TimeSlot");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.IsAvailable)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Specialty)
            .WithMany()
            .HasForeignKey(x => x.SpecialtyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Appointment)
            .WithOne()
            .HasForeignKey<TimeSlot>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
