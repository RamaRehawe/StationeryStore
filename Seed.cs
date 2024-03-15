using StationeryStore.Data;
using StationeryStore.Models;
using System;
using System.Collections.Generic;

namespace StationeryStore
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            
            if (!dataContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Username = "admin",
                        Password = "admin123",
                        Email = "admin@example.com",
                        Phone = "1234567890",
                        UserType = "Admin"
                    },
                    new User
                    {
                        Username = "item_manager",
                        Password = "item123",
                        Email = "manager@example.com",
                        Phone = "9876543210",
                        UserType = "Item Manager"
                    },
                    new User
                    {
                        Username = "driver1",
                        Password = "driver123",
                        Email = "driver1@example.com",
                        Phone = "5555555555",
                        UserType = "Driver"
                    },
                    new User
                    {
                        Username = "customer1",
                        Password = "customer123",
                        Email = "customer1@example.com",
                        Phone = "9999999999",
                        UserType = "Customer"
                    }
                };


                dataContext.Users.AddRange(users);
                dataContext.SaveChanges();
            }

            if (!dataContext.Addresses.Any())
            {
                var addresses = new List<Address>
                {
                    // Sample data for addresses
                    new Address
                    {
                        Title = "Home",
                        City = "City1",
                        Street = "Street1",
                        Name = "John Doe",
                        Phone = "1234567890",
                        UserId = 4
                    },
                    new Address
                    {
                        Title = "Work",
                        City = "City2",
                        Street = "Street2",
                        Name = "Jane Doe",
                        Phone = "9876543210",
                        UserId = 4
                    }
                };

                dataContext.Addresses.AddRange(addresses);
                dataContext.SaveChanges();
            }

            if (!dataContext.Admins.Any())
            {
                var admins = new List<Admin>
                {
                    // Sample data for admins
                    new Admin
                    {
                        
                        UserId = 1 // Admin user with id 1
                    }
                };

                dataContext.Admins.AddRange(admins);
                dataContext.SaveChanges();
            }

            if (!dataContext.Atributes.Any())
            {
                var attributes = new List<Atribute>
                {
                    // Sample data for attributes
                    new Atribute { Name = "Color" },
                    new Atribute { Name = "Size" }
                };
                dataContext.Atributes.AddRange(attributes);
                dataContext.SaveChanges();
            }

            if (!dataContext.Carts.Any())
            {
                var carts = new List<Cart>
                {
                    // Sample data for carts
                    new Cart { UserId = 4 } // Sample data for a cart belonging to a customer with id 4
                };

                dataContext.Carts.AddRange(carts);
                dataContext.SaveChanges();
            }
            if (!dataContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    // Sample data for categories
                    new Category { Name = "Category1" }, // Sample data for a category with name "Category1"
                    new Category { Name = "Category2" }, // Sample data for a category with name "Category2"
                };

                dataContext.Categories.AddRange(categories);
                dataContext.SaveChanges();
            }
            if (!dataContext.SubCategories.Any())
            {
                var subCategories = new List<SubCategory>
                {
                    // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "SubCategory1",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "SubCategory2",
                        CategoryId = 2 // Assuming this is the ID of the corresponding category
                    },

                };

                dataContext.SubCategories.AddRange(subCategories);
                dataContext.SaveChanges();
            }
            if (!dataContext.Products.Any())
            {
                var products = new List<Product>
                {
                    // Sample data for products
                    new Product
                    {
                        Name = "Product1",
                        Description = "Description1",
                        SubCategoryId = 1 // Assuming this is the ID of the corresponding sub-category
                    },
                    new Product
                    {
                        Name = "Product2",
                        Description = "Description2",
                        SubCategoryId = 2 // Assuming this is the ID of the corresponding sub-category
                    },
                };

                dataContext.Products.AddRange(products);
                dataContext.SaveChanges();
            }

            if (!dataContext.CartItems.Any())
            {
                var cartItems = new List<CartItem>
                {
                    // Sample data for cart items
                    new CartItem { Quantity = 1, ProducAttributeQuanttId = 1 , CartId = 1 }, // Sample data for a cart item with quantity 1, belonging to cart with id 1, and associated with product with id 1
                    new CartItem { Quantity = 2, ProducAttributeQuanttId = 2 , CartId = 2 }, // Sample data for a cart item with quantity 2, belonging to cart with id 2, and associated with product with id 2
                };

                dataContext.CartItems.AddRange(cartItems);
                dataContext.SaveChanges();
            }

            

            if (!dataContext.ContactInformation.Any())
            {
                var contactInfo = new List<ContactInformation>
                {
                    // Sample data for contact information
                    new ContactInformation
                    {
                        Type = "Phone",
                        Value = "1234567890",
                        Description = "Main phone number"
                    },
                    new ContactInformation
                    {
                        Type = "Email",
                        Value = "contact@example.com",
                        Description = "Main email address"
                    },
                    new ContactInformation
                    {
                        Type = "SocialMedia",
                        Value = "https://www.facebook.com/example",
                        Description = "Facebook page"
                    },
                };

                dataContext.ContactInformation.AddRange(contactInfo);
                dataContext.SaveChanges();
            }

            if (!dataContext.CustomerServices.Any())
            {
                var customerServices = new List<CustomerService>
                {
                    // Sample data for customer services
                    new CustomerService
                    {
                        Type = true,
                        Details = "General questions and inquiries",
                        AdminResponse = true,
                        UserId = 4 // Assuming this is the ID of the corresponding user
                    },
                    new CustomerService
                    {
                        Type = false,
                        Details = "Complaints and issues",
                        AdminResponse = false,
                        UserId = 4 // Assuming this is the ID of the corresponding user
                    },
                };

                dataContext.CustomerServices.AddRange(customerServices);
                dataContext.SaveChanges();
            }

            if (!dataContext.Drivers.Any())
            {
                var drivers = new List<Driver>
                {
                    // Sample data for drivers
                    new Driver
                    {
                        DriverStatus = true,
                        DriverLicense = "DL12345",
                        UserId = 3 // Assuming this is the ID of the corresponding user
                    },
                };

                dataContext.Drivers.AddRange(drivers);
                dataContext.SaveChanges();
            }

            if (!dataContext.ImageAttributes.Any())
            {
                var imageAttributes  = new List<ImageAttribute>
                {
                    // Sample data for images attributes
                    new ImageAttribute
                    {
                        URL = "image1.jpg",
                        ProductId = 1 // Assuming this is the ID of the corresponding product
                    },
                    new ImageAttribute
                    {
                        URL = "image2.jpg",
                        ProductId = 2 // Assuming this is the ID of the corresponding product
                    },
                };

                dataContext.ImageAttributes.AddRange(imageAttributes);
                dataContext.SaveChanges();
            }

            if (!dataContext.ProductAttributesQuantities.Any())
            {
                var productAttributeQuantities = new List<ProductAttributeQuantity>
                {
                    // Sample data for product attribute quantities
                    new ProductAttributeQuantity
                    {
                        Quantity = 10,
                        Price = 5.00,
                        ProductId = 3 // Assuming this is the ID of the corresponding product
                    },
                    new ProductAttributeQuantity
                    {
                        Quantity = 20,
                        Price = 10.00,
                        ProductId = 2 // Assuming this is the ID of the corresponding product
                    },
                };

                dataContext.ProductAttributesQuantities.AddRange(productAttributeQuantities);
                dataContext.SaveChanges();
            }


            if (!dataContext.Orders.Any())
            {
                var orders = new List<Order>
                {
                    // Sample data for orders
                    new Order
                    {
                        OrderDate = DateTime.Now,
                        TotalAmount = 100.00,
                        OrderStatus = "Pending",
                        ShippingCost = 10,
                        UserId = 4, // Assuming this is the ID of the corresponding user
                        AddressId = 1, // Assuming this is the ID of the corresponding address
                        DriverId = 1, // Assuming this is the ID of the corresponding driver
                        FailDeliver = "None", // Assuming there's a default value for this property
                    },
                };

                dataContext.Orders.AddRange(orders);
                dataContext.SaveChanges();
            }

            if (!dataContext.OrderItems.Any())
            {
                var orderItems = new List<OrderItem>
                {
                    // Sample data for order items
                    new OrderItem
                    {
                        Quantity = 1,
                        Price = 50.00,
                        OrderId = 2, // Assuming this is the ID of the corresponding order
                        ProducAttributeQuanttId = 2 // Assuming this is the ID of the corresponding product
                    },
                    new OrderItem
                    {
                        Quantity = 2,
                        Price = 25.00,
                        OrderId = 2, // Assuming this is the ID of the corresponding order
                        ProducAttributeQuanttId = 3 // Assuming this is the ID of the corresponding product
                    },
                };

                dataContext.OrderItems.AddRange(orderItems);
                dataContext.SaveChanges();
            }

            if (!dataContext.ProductAttributes.Any())
            {
                var productAttributes = new List<ProductAttribute>
                {
                    // Sample data for product attributes
                    new ProductAttribute
                    {
                        Value = "Red",
                        ProductAttributeQuantityId = 1, // Assuming this is the ID of the corresponding product attribute quantity
                        AttributeId = 1 // Assuming this is the ID of the corresponding attribute
                    },
                    new ProductAttribute
                    {
                        Value = "Large",
                        ProductAttributeQuantityId = 2, // Assuming this is the ID of the corresponding product attribute quantity
                        AttributeId = 2 // Assuming this is the ID of the corresponding attribute
                    },
                };

                dataContext.ProductAttributes.AddRange(productAttributes);
                dataContext.SaveChanges();
            }

            

            

            if (!dataContext.Rates.Any())
            {
                var rates = new List<Rate>
                {
                    // Sample data for rates
                    new Rate
                    {
                        Rating = 4.5,
                        Date = DateTime.Now,
                        UserId = 4, // Assuming this is the ID of the corresponding user
                        ProductId = 3 // Assuming this is the ID of the corresponding product
                    },
                    new Rate
                    {
                        Rating = 5.0,
                        Date = DateTime.Now,
                        UserId = 4, // Assuming this is the ID of the corresponding user
                        ProductId = 2 // Assuming this is the ID of the corresponding product
                    },
                };

                dataContext.Rates.AddRange(rates);
                dataContext.SaveChanges();
            }

            if (!dataContext.Reviews.Any())
            {
                var reviews = new List<Review>
                {
                    // Sample data for reviews
                    new Review
                    {
                        Text = "Great product!",
                        Date = DateTime.Now,
                        UserId = 4, // Assuming this is the ID of the corresponding user
                        ProductId = 3 // Assuming this is the ID of the corresponding product
                    },
                    new Review
                    {
                        Text = "Excellent service!",
                        Date = DateTime.Now,
                        UserId = 4, // Assuming this is the ID of the corresponding user
                        ProductId = 2 // Assuming this is the ID of the corresponding product
                    },
                };

                dataContext.Reviews.AddRange(reviews);
                dataContext.SaveChanges();
            }

            
        }
    
    
    }
}
