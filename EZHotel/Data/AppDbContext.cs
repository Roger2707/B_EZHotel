using EZHotel.DTOs.Rooms;
using EZHotel.DTOs.Users;
using EZHotel.Helpers;
using EZHotel.Models;
using EZHotel.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace EZHotel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        } 
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<Manager> Managers => Set<Manager>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(10, 2);

            // Table Per Type mapping
            modelBuilder.Entity<User>().UseTptMappingStrategy();

            // Role mapping
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Staff mapping
            modelBuilder.Entity<Staff>()
                .Property(s => s.Salary)
                .HasPrecision(10, 2);

            // Manager mapping
            modelBuilder.Entity<Manager>()
                .Property(m => m.ApprovalLimit)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Manager>()
                .Property(m => m.PerformanceBonusRate)
                .HasPrecision(10, 2);

            #region Seeds

            var roleManagerId = Guid.NewGuid();
            var roleStaffId = Guid.NewGuid();
            var roleCustomerId = Guid.NewGuid();

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = roleManagerId, Name = "Manager" },
                new Role { Id = roleStaffId, Name = "Staff" },
                new Role { Id = roleCustomerId, Name = "Customer" }
            );

            BF.roles.AddRange(
                new List<Role>
                {
                    new Role { Id = roleManagerId, Name = "Manager" },
                    new Role { Id = roleStaffId, Name = "Staff" },
                    new Role { Id = roleCustomerId, Name = "Customer" }
                }
            );

            var staffId = Guid.NewGuid();
            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                Id = staffId,
                FullName = "Simon Nguyen",
                UserName = "simon",
                HashPassword = BF.HashPassword("simon@123"),
                Email = "staff@example.com",
                PhoneNumber = "0987654321",
                Birthday = new DateTime(1993, 06, 01),
                Address = "456 Main St",
                Avatar = "",
                PublicId = "",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = roleStaffId,
                EmployeeCode = "EMP001",
                Position = "Leader - Customer Service",
                HireDate = new DateTime(2020, 1, 1),
                Salary = 500m,
                BankAccount = "1234567890",
                EmergencyContact = "0901234567",
                Certifications = "Hotel Management"
            });

            var managerId = Guid.NewGuid();
            modelBuilder.Entity<Manager>().HasData(new Manager
            {
                Id = managerId,
                FullName = "Roger Huynh",
                UserName = "roger",
                HashPassword = BF.HashPassword("roger@123"),
                Email = "rogerhuynh2707@gmail.com",
                PhoneNumber = "0776198888",
                Birthday = new DateTime(1999, 07, 27),
                Address = "789 Main St",
                Avatar = "",
                PublicId = "",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = roleManagerId,
                EmployeeCode = "MGR001",
                Position = "Hotel Manager",
                HireDate = new DateTime(2015, 5, 5),
                Salary = 2000m,
                BankAccount = "9876543210",
                EmergencyContact = "0909876543",
                Certifications = "Leadership",
                ApprovalLimit = 5000m,
                PerformanceBonusRate = 0.1m,
                AuthorityLevel = 3
            });

            var customerId = Guid.NewGuid();
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = customerId,
                FullName = "Quincy Thai",
                UserName = "quincy",
                HashPassword = BF.HashPassword("quincy@123"),
                Email = "customer@example.com",
                PhoneNumber = "0934567890",
                Birthday = new DateTime(1995, 10, 16),
                Address = "321 Main St",
                Avatar = "",
                PublicId = "",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = roleCustomerId,
                LoyaltyPoints = 100,
                Nationality = "Vietnam",
                PassportNumber = "A12345678",
                PreferredRoomType = RoomType.Vip,
                PaymentMethod = PaymentMethodType.CreditCard,
                SpecialRequest = "Late check-in"
            });

            #endregion
        }
    }
}
