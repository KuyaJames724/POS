using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UserData;
using PasswordHasher;



using Microsoft.Extensions.Logging.Abstractions;




public class _DbContext : DbContext
{

     private readonly IConfiguration _configuration;

    public _DbContext(DbContextOptions<_DbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public _DbContext(DbContextOptions<_DbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public object Products { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("_DbContext"));
        }
    }
    // DbSet properties
    
    

     // Method for seeding tables
     public void SeedData()
    {
        

        InsertInitialUsers();
        // Other tables to seed here
    }

     public void InsertInitialUsers()
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

