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
            
            //if (!dataContext.Users.Any())
            //{
            //    var users = new List<User>
            //    {
            //        new User
            //        {
            //            Username = "admin",
            //            Password = "admin123",
            //            Email = "admin@example.com",
            //            Phone = "1234567890",
            //            UserType = "Admin",
            //        JwtToken = null, // assuming no token initially
            //        Birthdate = null, // assuming no birthdate initially
            //        Gender = null // assuming no gender initially
            //        }   
                   
            //    };


            //    dataContext.Users.AddRange(users);
            //    dataContext.SaveChanges();
            //}

            

            //if (!dataContext.Admins.Any())
            //{
            //    var admins = new List<Admin>
            //    {
            //        // Sample data for admins
            //        new Admin
            //        {
                        
            //            UserId = 1 // Admin user with id 1
            //        }
            //    };

            //    dataContext.Admins.AddRange(admins);
            //    dataContext.SaveChanges();
            //}

            //if (!dataContext.Atributes.Any())
            //{
            //    var attributes = new List<Atribute>
            //    {
            //        // Sample data for attributes
            //        new Atribute { Name = "لون" },
            //        new Atribute { Name = "قياس" },
            //         new Atribute { Name = "عدد الصفحات" },
            //          new Atribute { Name = "عدد الأقلام" },
            //           new Atribute { Name = "بلد المنشأ" },
            //            new Atribute { Name = "عدد العناصر" },
            //             new Atribute { Name = "النوع" },
            //              new Atribute { Name = "نوع الغلاف" },
            //    };
            //    dataContext.Atributes.AddRange(attributes);
            //    dataContext.SaveChanges();
            //}

            //if (!dataContext.Categories.Any())
            //{
            //    var categories = new List<Category>
            //    {
            //        // Sample data for categories
            //        new Category { Name = "أقلام و أدوات كتابة" }, // Sample data for a category with name "Category1"
            //        new Category { Name = "دفاتر و ملاحظات" }, // Sample data for a category with name "Category2"
            //         new Category { Name = "أدوات الرسم" }, // Sample data 
            //          new Category { Name = "أدوات هندسية" }, // Sample data 
            //           new Category { Name = "أدوات مكتبية" }, // Sample data 
            //            new Category { Name = "أوراق و مفكرات" }, // Sample data 
            //             new Category { Name = "حقائب مدرسية" }, // Sample data 
            //    };

            //    dataContext.Categories.AddRange(categories);
            //    dataContext.SaveChanges();
            //}
                     

            //if (!dataContext.ContactInformation.Any())
            //{
            //    var contactInfo = new List<ContactInformation>
            //    {
            //        // Sample data for contact information
            //        new ContactInformation
            //        {
            //            Type = "Phone",
            //            Value = "+1234567890",
            //            Description = "Main phone number"
            //        },
            //        new ContactInformation
            //        {
            //            Type = "Email",
            //            Value = "Info@StationeryStore.com",
            //            Description = "Main email address"
            //        },
            //        new ContactInformation
            //        {
            //            Type = "Social Media",
            //            Value = "https://www.facebook.com/profile.php?id=61558642717289&mibextid=ZbWKwL",
            //            Description = "Facebook page"
            //        },
            //    };

            //    dataContext.ContactInformation.AddRange(contactInfo);
            //    dataContext.SaveChanges();
            //}


           

            



            

            

          
            


        }



    }

}
