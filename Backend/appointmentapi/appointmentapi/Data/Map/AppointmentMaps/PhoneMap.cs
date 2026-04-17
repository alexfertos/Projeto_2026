using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps
{
    public class PhoneMap : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phone");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number)
            .IsRequired()
            .HasMaxLength(15);
        }
    }
}
