using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps;

public class UnitScheduleMap : IEntityTypeConfiguration<UnitSchedule>
{
    public void Configure(EntityTypeBuilder<UnitSchedule> builder)
    {
        builder.ToTable("UnitSchedule");

        builder.HasKey(us => us.Id);

        builder.Property(us => us.DayOfWeek)
            .IsRequired()
            .HasMaxLength(15); // ex: "Segunda-feira", "Domingo"

        builder.Property(us => us.StartTime)
            .IsRequired();

        builder.Property(us => us.EndTime)
            .IsRequired();

        builder.Property(us => us.IsAvailable)
            .IsRequired();

        // 🔹 Relacionamento com Unit (1:N)
        builder.HasOne(us => us.Unit)
            .WithMany(u => u.UnitSchedules)
            .HasForeignKey(us => us.UnitId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}