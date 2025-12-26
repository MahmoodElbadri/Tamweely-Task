using Microsoft.EntityFrameworkCore;
using Tamweely.Domain.Entities;

namespace Tamweely.Infrastructure.Data;

public class TamweelyDbContext(DbContextOptions<TamweelyDbContext> options): DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<AddressEntry> AddressEntries { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AddressEntry>()
            .HasOne(tmp => tmp.JobTitle)
            .WithMany(tmp => tmp.Addresses)
            .HasForeignKey(tmp=>tmp.JobTitleId)
            .OnDelete(DeleteBehavior.Restrict); //to prevent deleting address when jobtitle is deleted

        modelBuilder.Entity<AddressEntry>()
            .HasOne(tmp => tmp.Department)
            .WithMany(tmp => tmp.Addresses)
            .OnDelete(DeleteBehavior.Restrict); //to prevent deleting address when department is deleted

        //Some Seed Data 
        modelBuilder.Entity<Department>().HasData(
        new Department { Id = 1, Name = "IT" },
        new Department { Id = 2, Name = "HR" },
        new Department { Id = 3, Name = "Finance" },
        new Department { Id = 4, Name = "Marketing" }
    );

        // 3. Seeding JobTitles
        modelBuilder.Entity<JobTitle>().HasData(
            new JobTitle { Id = 1, Name = "Senior Backend Developer" },
            new JobTitle { Id = 2, Name = "Frontend Developer" },
            new JobTitle { Id = 3, Name = "HR Specialist" },
            new JobTitle { Id = 4, Name = "Accountant" },
            new JobTitle { Id = 5, Name = "Marketing Manager" }
        );

        // 4. Seeding Employees (AddressEntries)
        modelBuilder.Entity<AddressEntry>().HasData(
            new AddressEntry
            {
                Id = 1,
                Fullname = "Mahmood Elbadri",
                Email = "mahmood@tamweely.com",
                MobileNumber = "01000000001",
                Address = "Cairo, Egypt",
                DateOfBirth = new DateTime(1998, 5, 20),
                DepartmentId = 1, // IT
                JobTitleId = 1,   // Senior Backend Developer
                PhotoPath = null,
            },
            new AddressEntry
            {
                Id = 2,
                Fullname = "Sarah Ahmed",
                Email = "sarah@tamweely.com",
                MobileNumber = "01200000002",
                Address = "Giza, Egypt",
                DateOfBirth = new DateTime(2000, 10, 15),
                DepartmentId = 2, // HR
                JobTitleId = 3,   // HR Specialist
                PhotoPath = null,
            }
        );
    }
}
