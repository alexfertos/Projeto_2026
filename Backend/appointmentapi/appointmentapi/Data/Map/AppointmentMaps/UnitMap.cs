using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps
{
    public class UnitMap : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Unit");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(u => u.Address)
                .WithOne()//fk
                .HasForeignKey<Unit>(u => u.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Phone)
            .WithOne()
            .HasForeignKey<Unit>(u => u.PhoneId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(u => u.UnitSchedules)
                .WithOne(us => us.Unit) //fk
                .HasForeignKey(us => us.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UnitSpecialties)
                .WithOne(us => us.Unit) //fk
                .HasForeignKey(us => us.UnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
