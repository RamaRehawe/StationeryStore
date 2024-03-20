using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StationeryStore.Models;

namespace StationeryStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Atribute> Atributes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<ImageAttribute> ImageAttributes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeQuantity> ProductAttributesQuantities { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
            // one to many
            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.CategoryId);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SubCategoryId);
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.ProductAttributeQuantity)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductAttributeQuantityId);
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Cart)
                .WithMany(ca => ca.CartItems)
                .HasForeignKey(c => c.CartId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(or => or.OrderItems)
                .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.ProductAttributeQuantity)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(o => o.ProductAttributeQuantityId);
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Rates)
                .HasForeignKey(r => r.ProductId);
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.UserId);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);
            
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Attribute)
                .WithMany(a => a.ProductAttributes)
                .HasForeignKey(pa => pa.AttributeId);
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.ProductAttributeQuantity)
                .WithMany(q => q.ProductAttributes)
                .HasForeignKey(pa => pa.ProductAttributeQuantityId);
            modelBuilder.Entity<ProductAttributeQuantity>()
                .HasOne(q => q.Product)
                .WithMany(p => p.ProductAttributeQuantities)
                .HasForeignKey(q => q.ProductId);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Driver)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DriverId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ImageAttribute>()
                .HasOne(i => i.ProductAttributeQuantity)
                .WithMany(p => p.ImageAttributes)
                .HasForeignKey(i => i.ProductAttributeQuantityId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<CustomerService>()
                .HasOne(c => c.User)
                .WithMany(u => u.CustomerServices)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Address)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.AddressId);

            // one to one
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Driver)
                .WithOne(d => d.User)
                .HasForeignKey<Driver>(d => d.UserId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId);
        }
    }
}