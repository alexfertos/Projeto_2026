using appointmentapi.Models.AppointmentEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmentapi.Data.Map.AppointmentMaps;

public class AppointmentMap : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Date).IsRequired();

        builder.HasOne(a => a.Person) 
               .WithMany() 
               .HasForeignKey(a => a.PersonId) 
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Specialty)
               .WithMany()
               .HasForeignKey(a => a.SpecialtyId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Unit)
               .WithMany()
               .HasForeignKey(a => a.UnitId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(a => a.Address)
               .WithMany()
               .HasForeignKey(a => a.AddressId)
               .IsRequired(false) 
               .OnDelete(DeleteBehavior.SetNull); 


        builder.HasOne(a => a.Phone)
               .WithMany()
               .HasForeignKey(a => a.PhoneId)
               .IsRequired(false) 
               .OnDelete(DeleteBehavior.SetNull);

    }
}