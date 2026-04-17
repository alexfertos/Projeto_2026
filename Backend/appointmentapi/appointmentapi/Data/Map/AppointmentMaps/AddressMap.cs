using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Number)
            .IsRequired();

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(a => a.State)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(a => a.ZipCode)
            .IsRequired()
            .HasMaxLength(12);

        builder.HasIndex(a => a.ZipCode);
    }
}
