using Microsoft.EntityFrameworkCore;
using SportEdge.API.Models.Domain;

namespace SportEdge.API.Data
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the application.
    /// Manages the database connection and provides access to the application's domain models.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SizeOption> SizeOptions { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


        //Novo dodano
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        //Novo dodano
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Gender>().HasData(
              new { Id = 1, Name = "Mens" },
              new { Id = 2, Name = "Womens" },
              new { Id = 3, Name = "Kids" },
              new { Id = 4, Name = "Unisex" }
              );
            */


            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProductVariation>()
                .HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariations)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Products can be deleted and cascade to variations

            
            modelBuilder.Entity<ProductVariation>()
                .HasOne(pv => pv.SizeOption)
                .WithMany(so => so.ProductVariations)
                .HasForeignKey(pv => pv.SizeOptionId)
                .OnDelete(DeleteBehavior.Restrict); // SizeOptions are fixed/static

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            /*
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            */// When a user is deleted, their cart(s) are deleted too

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Cart deletion removes cart items

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ProductVariation)
                .WithMany(pv => pv.CartItems)
                .HasForeignKey(ci => ci.ProductVariationId)
                .OnDelete(DeleteBehavior.Restrict);


            // Order -> User: Cascade delete orders when user is deleted
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> Order: Cascade delete order items when order is deleted
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem -> ProductVariation: Do NOT delete product variation when order item is deleted
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductVariation)
                .WithMany() // no navigation property needed on ProductVariation side
                .HasForeignKey(oi => oi.ProductVariationId)
                .OnDelete(DeleteBehavior.Restrict); // or ClientSetNull if you prefer

        }
    }
}
