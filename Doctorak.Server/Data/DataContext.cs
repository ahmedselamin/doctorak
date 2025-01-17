﻿namespace Doctorak.Server.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
    public DbSet<BreakSlot> Breaks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Doctor>().ToTable("Doctors");  //separate table

        modelBuilder.Entity<User>()
            .HasMany(u => u.Appointments)
            .WithOne()
            .HasForeignKey(a => a.UserId);   //user & appointments, 1 => n

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Appointments)
            .WithOne()
            .HasForeignKey(a => a.DoctorId);   // doctor & appointments, 1 => n

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.AvailabilitySlots)
            .WithOne()
            .HasForeignKey(a => a.DoctorId);   // doctor & slots, 1 => n

        modelBuilder.Entity<AvailabilitySlot>()
            .HasMany(a => a.Breaks)
            .WithOne()
            .HasForeignKey(b => b.AvailabilitySlotId);   //availability and break, 1 => n

    }
}
