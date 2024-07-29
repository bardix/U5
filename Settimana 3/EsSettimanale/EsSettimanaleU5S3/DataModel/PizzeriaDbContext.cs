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

    }

}
