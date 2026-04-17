using appointmentapi.Models.AppointmentEntity;
using appointmentapi.Models.AuthEntity;
using Microsoft.EntityFrameworkCore;

namespace appointmentapi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Person> Persons { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresss { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<UnitSchedule> UnitSchedules { get; set; }
    public DbSet<UnitSpecialty> UnitSpecialties { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

