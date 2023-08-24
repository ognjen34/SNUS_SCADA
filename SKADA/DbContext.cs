using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System;
using SKADA.Models.Users.Model;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Outputs.Model;
using SKADA.Models.Devices.Model;

public class AppDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Device> Device { get; set; }
    public DbSet<AnalogInput> AnalogInput { get; set; }
    public DbSet<AnalogOutput> AnalogOutput { get; set; }
    public DbSet<DigitalInput> DigitalInput { get; set; }
    public DbSet<DigitalOutput> DigitalOutput { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("HCI");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
    public  void InitDataBase()
    {
        Users.AddAsync(
             new User
             (
                 "Pera",
                 "Peric",
                 "pera.client@example.com",
                 "pera23",
                 UserType.CLIENT

             ));
        Users.AddAsync(
            new User
             (
                 "Goran",
                 "Gogic",
                 "gogi.admin@example.com",
                 "admin123",
                 UserType.ADMIN
             ));
        this.SaveChanges();
    }
}