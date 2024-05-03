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
            SeedDataContext();

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
                        UserType = "Admin",
                    JwtToken = null, // assuming no token initially
                    Birthdate = null, // assuming no birthdate initially
                    Gender = null // assuming no gender initially
                    }

                };


                dataContext.Users.AddRange(users);
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
                    new Atribute { Name = "لون" },
                    new Atribute { Name = "قياس" },
                     new Atribute { Name = "عدد الصفحات" },
                      new Atribute { Name = "عدد الأقلام" },
                       new Atribute { Name = "بلد المنشأ" },
                        new Atribute { Name = "عدد العناصر" },
                         new Atribute { Name = "النوع" },
                          new Atribute { Name = "نوع الغلاف" },
                };
                dataContext.Atributes.AddRange(attributes);
                dataContext.SaveChanges();
            }

            if (!dataContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    // Sample data for categories
                    new Category { Name = "أقلام و أدوات كتابة" }, // Sample data for a category with name "Category1"
                    new Category { Name = "دفاتر و ملاحظات" }, // Sample data for a category with name "Category2"
                     new Category { Name = "أدوات الرسم" }, // Sample data 
                      new Category { Name = "أدوات هندسية" }, // Sample data 
                       new Category { Name = "أدوات مكتبية" }, // Sample data 
                        new Category { Name = "أوراق و مفكرات" }, // Sample data 
                         new Category { Name = "حقائب مدرسية" }, // Sample data 
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
                        Name = "أقلام حبر",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "أقلام رصاص",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                      // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "أقلام جافة",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "أقلام جيل",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                      // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "أقلام تحديد",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "أقلام تخطيط",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                      // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "أقلام تصحيح",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "أقلام ملونة",
                        CategoryId = 1 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "دفاتر عادية",
                        CategoryId = 2 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "دفاتر سلك",
                        CategoryId = 2 // Assuming this is the ID of the corresponding category
                    },
                      // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "دفاتر رسم",
                        CategoryId = 2 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "دفاتر رسم بياني",
                        CategoryId = 2 // Assuming this is the ID of the corresponding category
                    },

                     new SubCategory
                    {
                        Name = "أقلام حبر فنية",
                        CategoryId = 3 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "فُرش فنية",
                        CategoryId = 3 // Assuming this is the ID of the corresponding category
                    },
                      // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "ألوان مائية",
                        CategoryId = 3 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "ألوان خشبية",
                        CategoryId = 3 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "ألوان شمعية",
                        CategoryId = 3 // Assuming this is the ID of the corresponding category
                    },

                      new SubCategory
                    {
                        Name = "أدوات رسم هندسي",
                        CategoryId = 4 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "آلالات حاسبة علمية ",
                        CategoryId = 4 // Assuming this is the ID of the corresponding category
                    },


                      new SubCategory
                    {
                        Name = "ملصقات و فاصلات",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "دباسات و مزيلات",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },
                      // Sample data for sub-categories
                    new SubCategory
                    {
                        Name = "مقصات و مشارط",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },
                    new SubCategory
                    {
                        Name = "الآلات حاسبة",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "مصنفات",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },

                      new SubCategory
                    {
                        Name = "ممحاة و مبراة",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "مستلزمات إضافية ",
                        CategoryId = 5 // Assuming this is the ID of the corresponding category
                    },

                      new SubCategory
                    {
                        Name = "أوراق طباعة",
                        CategoryId = 6 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "أوراق ملونة",
                        CategoryId = 6 // Assuming this is the ID of the corresponding category
                    },

                      new SubCategory
                    {
                        Name = "أوراق ملاحظات",
                        CategoryId = 6 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "مفكرات يومية ",
                        CategoryId = 6 // Assuming this is the ID of the corresponding category
                    },

                       new SubCategory
                    {
                        Name = "حقائب ظهر مدرسية",
                        CategoryId = 7 // Assuming this is the ID of the corresponding category
                    },

                      new SubCategory
                    {
                        Name = "حقائب أقلام",
                        CategoryId = 7 // Assuming this is the ID of the corresponding category
                    },
                     new SubCategory
                    {
                        Name = "حقائب مستلزمات مدرسية ",
                        CategoryId = 7 // Assuming this is the ID of the corresponding category
                    },
                };

                dataContext.SubCategories.AddRange(subCategories);
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
                        Value = "+1234567890",
                        Description = "Main phone number"
                    },
                    new ContactInformation
                    {
                        Type = "Email",
                        Value = "Info@StationeryStore.com",
                        Description = "Main email address"
                    },
                    new ContactInformation
                    {
                        Type = "Social Media",
                        Value = "https://www.facebook.com/profile.php?id=61558642717289&mibextid=ZbWKwL",
                        Description = "Facebook page"
                    },
                };

                dataContext.ContactInformation.AddRange(contactInfo);
                dataContext.SaveChanges();
            }















        }



    }

}
