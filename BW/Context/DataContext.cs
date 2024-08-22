using BW2_Team6.Models;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Animal> Animals { get; set; }

        public virtual DbSet<Owner> Owners { get; set; }

        public virtual DbSet<Visit> Visits { get; set; }

        public virtual DbSet<Recover> Recovers { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Sell> Sells { get; set; }
        public virtual DbSet<Locker> Locker { get; set; }
        public virtual DbSet<Drawer> Drawers { get; set; }

        public DbSet<DrawerProduct> DrawerProducts { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Roles)
                .WithMany(i => i.Users)
                .UsingEntity(j => j.ToTable("RoleUsers"));


            modelBuilder.Entity<Animal>()
                .HasIndex(a => a.Microchip)
                .IsUnique();


            modelBuilder.Entity<Sell>()
                .HasIndex(s => s.NumberOfRecipe)
                .IsUnique();

            modelBuilder.Entity<Locker>()
                .HasIndex(s => s.NumberLocker)
                .IsUnique();

            modelBuilder.Entity<DrawerProduct>()
           .HasKey(dp => new { dp.ProductId, dp.DrawerId });

            modelBuilder.Entity<DrawerProduct>()
           .HasOne(dp => dp.Product)
           .WithMany(p => p.Drawer)
           .HasForeignKey(dp => dp.ProductId);

            modelBuilder.Entity<DrawerProduct>()
                .HasOne(dp => dp.Drawer)
                .WithMany(d => d.Product)
                .HasForeignKey(dp => dp.DrawerId);
        }
    }
}
