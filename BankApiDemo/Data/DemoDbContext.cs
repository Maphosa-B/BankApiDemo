using BankApiDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace BankApiDemo.Data
{
    public class DemoDbContext : IdentityDbContext<IdentityUser>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<WidrawalLog> WidrawalLogs { get; set; }     
        public DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Accout Types Seeding
            builder.Entity<AccountType>().HasData(
            new AccountType
            {
                Id = 1,
                Name = "Savings",
            },
            new AccountType
            {
                Id = 2,
                Name = "Fixed Deposit",
            },
            new AccountType
            {
                Id = 3,
                Name = "Cheque",
            });

            //Users Seeding
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "34cd777f-2e09-48e2-a38a-16070952a95d",
                UserName = "Maphosabonkosi.bm@gmail.com",
                Email = "Maphosabonkosi.bm@gmail.com",
                NormalizedEmail = "Maphosabonkosi.bm@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Password@12"),
                SecurityStamp = string.Empty,
                FirstName = "Bonginkosi",
                LastName = "Maphosa",
                DateOfBirth = DateTime.Now,
                IdNumber = "9829105752082",
                PhoneNumberConfirmed = true
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "34cd767f-2e09-48e2-a38a-16074952a95d",
                UserName = "Testuser@gmail.com",
                Email = "Testuser@gmail.com",
                NormalizedEmail = "Testuser@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Password@12"),
                SecurityStamp = string.Empty,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = DateTime.Now,
                IdNumber = "9709105752082",
            });

            //Accounts Seeding
            builder.Entity<Account>().HasData(new Account
            {
                AccountNumber = "2423252626",
                AccountTypeId = 1,
                IdentityUserId = "34cd777f-2e09-48e2-a38a-16070952a95d",
                AvailableBalance = 10000,
                IsActive = true,
                Id = 1
            },
            new Account
            {
                AccountNumber = "2423352776",
                AccountTypeId = 2,
                IdentityUserId = "34cd777f-2e09-48e2-a38a-16070952a95d",
                AvailableBalance = 3000,
                IsActive = true,
                Id = 2
            },
            new Account
            {
                AccountNumber = "2423352776",
                AccountTypeId = 2,
                IdentityUserId = "34cd767f-2e09-48e2-a38a-16074952a95d",
                AvailableBalance = 3000,
                IsActive = true,
                Id = 3
            },
            new Account
            {
                AccountNumber = "2423352776",
                AccountTypeId = 3,
                IdentityUserId = "34cd767f-2e09-48e2-a38a-16074952a95d",
                AvailableBalance = 3000,
                IsActive = true,
                Id = 4
            });
        }

    }
    
}
