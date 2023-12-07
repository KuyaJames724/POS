//using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UserData;
using PasswordHasher;



using Microsoft.Extensions.Logging.Abstractions;




public class _DbContext : DbContext
{
    public _DbContext(DbContextOptions<_DbContext> options) : base(options)
    {
    }

    // DbSet properties
    
    public DbSet<User> Users { get; set; }

     // Method for seeding tables
    // public void SeedData()
    //{
        

   // }

     public void InsertInitialProducts()
    {
        

        if (!Users.Any())
        {
        
            var user1 = new User
            {
                Username = "user1",
                Email = "user1@example.com",
                PasswordSalt = SaltGenerator.GenerateRandomSalt(),
                PasswordHash = PasswordHasher1.HashPassword("password123"),
                Role = "User", 
                // Other user properties
            };

            var user2 = new User
            {
                Username = "admin1",
                Email = "admin1@example.com",
                PasswordSalt = SaltGenerator.GenerateRandomSalt(),
                PasswordHash = PasswordHasher1.HashPassword("password321"),
                Role = "Admin",
            };

            Users.AddRange(user1, user2);
            SaveChanges();
        }

    
    }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.Entity<User>().ToTable("Users");
    }  
}

