using Microsoft.EntityFrameworkCore;
using SocialVeterinarySurgeon.Domain.Entities;

namespace SocialVeterinarySurgeon.DAL
{
    public class SocialVeterinarySurgeonDbContext : DbContext
    {
        public SocialVeterinarySurgeonDbContext(DbContextOptions contextOptions) : base(contextOptions) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John",
                    LastName = "Smith",
                    FromMediaInteractiva = false
                },
                new Employee
                {
                    Id = 2,
                    Name = "Matt",
                    LastName = "Heafy",
                    FromMediaInteractiva = false
                });

            modelBuilder.Entity<Pet>().HasData(
                new Pet
                {
                    Id = 1,
                    OwnerId = 1,
                    Name = "Max",
                    Type = AnimalType.Dog
                },
                new Pet
                {
                    Id = 2,
                    OwnerId = 1,
                    Name = "Garfield",
                    Type = AnimalType.Cat
                },
                new Pet
                {
                    Id = 3,
                    OwnerId = 2,
                    Name = "Miyuki",
                    Type = AnimalType.Dog
                },
                new Pet
                {
                    Id = 4,
                    OwnerId = 2,
                    Name = "Karl",
                    Type = AnimalType.Bird
                });
        }
    }
}
