using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System;
using SKADA.Models.Users.Model;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Outputs.Model;
using SKADA.Models.Devices.Model;
using SKADA.Models.Alarms.Model;

public class AppDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Device> Device { get; set; }
    public DbSet<DigitalReadInstance> DigitalReadInstances { get; set; }
    public DbSet<AnalogInput> AnalogInput { get; set; }
    public DbSet<Alarm> Alarms { get; set; }
    public DbSet<AlarmInstance> AlarmInstances { get; set; }
    public DbSet<AnalogOutput> AnalogOutput { get; set; }
    public DbSet<DigitalInput> DigitalInput { get; set; }
    public DbSet<DigitalOutput> DigitalOutput { get; set;}

    public DbSet<AnalogReadInstance> AnalogReadInstance { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("HCI");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Device>(entity =>
        {
            entity.OwnsOne(d => d.deviceConfig, cfg =>
            {
                cfg.Property(dc => dc.LowLimit);
                cfg.Property(dc => dc.HighLimit);
                cfg.Property(dc => dc.SimulationType);
            });
        });
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
        var lowAlarm = new Alarm(new Guid(),Alarm.AlarmType.LOW, Alarm.AlarmPriority.LOW, "L", 45);
        var highAlarm = new Alarm(new Guid(),Alarm.AlarmType.HIGH, Alarm.AlarmPriority.HIGH, "L", 55);

        Alarms.Add(lowAlarm);
        Alarms.Add(highAlarm);

        AnalogInput.AddAsync(
            new AnalogInput(
                Guid.NewGuid(), "Deskripcija 1", "uredjaj1", 1, true, "L", new List<Alarm> { lowAlarm, highAlarm }
            ));
        DigitalInput.AddAsync(
            new DigitalInput(
                new Guid(), "Deskripcija digitalnog", "uredjaj2", 1, true
                ));
        DeviceConfig deviceConfig1 = new DeviceConfig(0,100,SimulationType.RTU);
        DeviceConfig deviceConfig2 = new DeviceConfig(-1,-1,SimulationType.COS);
        Device.Add(new Device("uredjaj1", 0, SKADA.Models.Devices.Model.Device.DeviceType.RTU,deviceConfig1));
        Device.Add(new Device("uredjaj2", 0, SKADA.Models.Devices.Model.Device.DeviceType.SIMULATION,deviceConfig2));

        this.SaveChanges();
    }
}