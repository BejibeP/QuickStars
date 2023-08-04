using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickStars.MVApi.Domain.Data.Entities;

namespace QuickStars.MVApi.Domain
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<LogMessage> LogMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LogMessage>().HasKey(e => e.Id);
            builder.Entity<LogMessage>().Property(e => e.ModifiedOn).IsConcurrencyToken();

            OnModelInitialize(builder);

            base.OnModelCreating(builder);
        }

        private void OnModelInitialize(ModelBuilder builder)
        {
            //Create Roles
            var adminRole = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = IdentityRoles.Admin,
                NormalizedName = IdentityRoles.Admin.ToUpper()
            };
            var userRole = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = IdentityRoles.User,
                NormalizedName = IdentityRoles.User.ToUpper()
            };

            // Seeding AspNetRoles
            builder.Entity<IdentityRole>().HasData(adminRole, userRole);

            // Create Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "PassW0rD!")
            };

            //Seeding AspNetUsers
            builder.Entity<IdentityUser>().HasData(adminUser);

            var adminRoles = new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            };

            // Seeding Admin role to the Admin user
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}