using Microsoft.EntityFrameworkCore;

namespace EsSettimanaleU5S3.DataModel
{
    public class PizzeriaDbContext : DbContext
    {

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<IngredientProduct> IngredientProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }

        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=PizzeriaInForno;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IngredientProduct>()
                .HasKey(ip => new { ip.IngredientId, ip.ProductId });
            modelBuilder.Entity<IngredientProduct>()
                .HasOne(ip => ip.Ingredient)
                .WithMany(i => i.IngredientProducts)
                .HasForeignKey(ip => ip.IngredientId);
            modelBuilder.Entity<IngredientProduct>()
                .HasOne(ip => ip.Product)
                .WithMany(p => p.IngredientProducts)
                .HasForeignKey(ip => ip.ProductId);

            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new { ru.RoleId, ru.UserId });
            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(ru => ru.RoleId);
            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(ru => ru.UserId);
        }


    }

}
