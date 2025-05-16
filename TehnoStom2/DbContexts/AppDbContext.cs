using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TehnoStom2.Entities;
namespace TehnoStom2.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext()
    {
        SQLitePCL.Batteries_V2.Init();
        Database.EnsureCreated(); // Создает БД при первом обращении
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databasePath = Path.Combine(FileSystem.AppDataDirectory, "appdatabase.db");
        optionsBuilder.UseSqlite($"Filename={databasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка отношений и ограничений

        // Role
        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Title)
            .IsUnique();

        // Specialization
        modelBuilder.Entity<Specialization>()
            .HasIndex(s => s.Title)
            .IsUnique();

        // User
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Specialization)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Task
        modelBuilder.Entity<Order>()
            .HasOne(t => t.Specialization)
            .WithMany(s => s.Orders)
            .HasForeignKey(t => t.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>()
            .HasOne(t => t.User)
            .WithMany(s => s.Orders)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

