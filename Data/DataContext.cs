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
        public DbSet<Image> Images { get; set; }
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
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId);
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
                .HasOne(o => o.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(o => o.ProductId);
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
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId);
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Attribute)
                .WithMany(a => a.ProductAttributes)
                .HasForeignKey(pa => pa.AttributeId);
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Quantity)
                .WithMany(q => q.ProductAttributes)
                .HasForeignKey(pa => pa.QuantityId);
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
            modelBuilder.Entity<ImageAttribute>()
                .HasOne(ia => ia.Image)
                .WithMany(i => i.ImageAttributes)
                .HasForeignKey(ia => ia.ImageId);
            modelBuilder.Entity<CustomerService>()
                .HasOne(c => c.User)
                .WithMany(u => u.CustomerServices)
                .HasForeignKey(c => c.UserId);

            // one to one
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Order)
                .WithOne(o => o.Address)
                .HasForeignKey<Order>(o => o.AddressId);
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
