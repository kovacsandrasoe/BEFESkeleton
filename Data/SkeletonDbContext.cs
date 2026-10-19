using BEFESkeleton.Models;
using Microsoft.EntityFrameworkCore;

namespace BEFESkeleton.Data
{
    public class SkeletonDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public SkeletonDbContext(DbContextOptions<SkeletonDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasData(new List<Car>()
                {
                    new Car(){ Id = Guid.Parse("bb7c6360-cc28-48de-b2cf-7c23fe71f4c0"), Model = "Audi", Brand = "A3"},
                    new Car(){ Id = Guid.Parse("2da22ad0-7740-44eb-afb2-03ffad0433bd"), Model = "Bmw", Brand = "i3"},
                    new Car(){ Id = Guid.Parse("d2a59096-7207-4c08-afa4-4344ca6293c6"), Model = "Tesla", Brand = "Model Y"}
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
