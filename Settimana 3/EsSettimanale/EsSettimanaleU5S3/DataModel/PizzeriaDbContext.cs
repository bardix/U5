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

            // Configurazione delle chiavi composite
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

            // Configurazione della precisione per il campo TotalPrice di OrderItem
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Dati di base per i prodotti
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Margherita", PhotoUrl = "margherita.jpg", Price = 5.99M, DeliveryTime = 20 },
                new Product { Id = 2, Name = "Pepperoni", PhotoUrl = "pepperoni.jpg", Price = 7.99M, DeliveryTime = 25 },
                new Product { Id = 3, Name = "Quattro Formaggi", PhotoUrl = "quattro_formaggi.jpg", Price = 8.99M, DeliveryTime = 30 }
            );

            // Dati di base per gli ingredienti
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tomato" },
                new Ingredient { Id = 2, Name = "Cheese" },
                new Ingredient { Id = 3, Name = "Pepperoni" },
                new Ingredient { Id = 4, Name = "Mushrooms" }
            );

            // Dati di base per i ruoli
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            // Dati di base per gli utenti
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin User", Email = "admin@example.com", Password = "admin123" }, // Nota: le password dovrebbero essere hashate in produzione
                new User { Id = 2, Name = "Regular User", Email = "user@example.com", Password = "user123" }
            );

            // Relazioni utente-ruolo
            modelBuilder.Entity<RoleUser>().HasData(
                new RoleUser { RoleId = 1, UserId = 1 }, // Admin User -> Admin
                new RoleUser { RoleId = 2, UserId = 2 }  // Regular User -> User
            );

            // Relazioni prodotto-ingrediente
            modelBuilder.Entity<IngredientProduct>().HasData(
                new IngredientProduct { IngredientId = 1, ProductId = 1 }, // Tomato -> Margherita
                new IngredientProduct { IngredientId = 2, ProductId = 1 }, // Cheese -> Margherita
                new IngredientProduct { IngredientId = 3, ProductId = 2 }, // Pepperoni -> Pepperoni
                new IngredientProduct { IngredientId = 2, ProductId = 2 }, // Cheese -> Pepperoni
                new IngredientProduct { IngredientId = 2, ProductId = 3 }, // Cheese -> Quattro Formaggi
                new IngredientProduct { IngredientId = 4, ProductId = 3 }  // Mushrooms -> Quattro Formaggi
            );

            // Dati di base per gli ordini
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = 2, OrderDate = DateTime.Now, ShippingAddress = "123 Main St", Notes = "Leave at the door", IsCompleted = false }
            );

            // Dati di base per gli articoli dell'ordine
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, TotalPrice = 11.98M }, // 2x Margherita
                new OrderItem { Id = 2, OrderId = 1, ProductId = 3, Quantity = 1, TotalPrice = 8.99M }   // 1x Quattro Formaggi
            );
        }
    }
}
