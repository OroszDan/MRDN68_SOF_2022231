using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRDN68_SOF_2022231.Models;
using System.Reflection.Emit;

namespace MRDN68_SOF_2022231.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Resume> Resumes { get; set; }

        public DbSet<Workplace> Workplaces { get; set; }

        public DbSet<IdentityUser> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
            new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }
            );

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            IdentityUser admin = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "dani01@gmail.com",
                EmailConfirmed = true,
                UserName = "dani01@gmail.com",
                NormalizedUserName = "DANI01@GMAIL.COM"
            };
            admin.PasswordHash = ph.HashPassword(admin, "almafa123");
            builder.Entity<IdentityUser>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = admin.Id
            });

            Resume res1 = new Resume()
            {
                FirstName = "Pista",
                LastName = "Kiss",
                Age = 55,
                OwnerId = admin.Id,
                Description = "Én egy nagyon motivált ember vagyok aki már nagyon sok munkahelyen megfordult..."
            };

            Resume res2 = new Resume()
            {
                FirstName = "Géza",
                LastName = "Nagy",
                Age = 45,
                OwnerId = admin.Id,
                Description = "Sokáig nem álltam munkába mivel fontosnak tartottam minél több diploma megszerzését..."
            };

            Workplace workplace1 = new Workplace()
            {
                CompanyName = "Evosoft",
                City = "Miskolc",
                WorkedYears = 1,
                Role = "Software engineer",
                OwnerId = res1.Id
                 

            };

            Workplace workplace2 = new Workplace()
            {
                CompanyName = "Epam",
                City = "Budapest",
                WorkedYears = 2,
                Role = "Business analyst",
                OwnerId = res2.Id

            };

            Workplace workplace3 = new Workplace()
            {
                CompanyName = "Bosch",
                City = "München",
                WorkedYears = 6,
                Role = "Mechanical engineer",
                OwnerId = res1.Id

            };

            builder.Entity<Resume>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Workplace>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Workplace>().HasData(workplace1, workplace2, workplace3);
            builder.Entity<Resume>().HasData(res1, res2);

            base.OnModelCreating(builder);
        }
    }
}