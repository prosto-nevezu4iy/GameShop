namespace Core.Migrations
{
    using Core.Domain;
    using Core.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Core.Infrastructure.GameShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Core.Infrastructure.GameShopContext context)
        {
            /*context.Categories.AddOrUpdate(c => c.Name, 
                new Category() { Name = "Game systems", Alias = "game-systems" },
                new Category() { Name = "Gaming Peripherals", Alias = "gaming-peripherals" },
                new Category() { Name = "Test", Alias = "test" }
            );

            context.SaveChanges();

            context.Categories.AddOrUpdate(c => c.Name,
                new Category() { Name = "Notebooks", Alias = "notebooks", ParentId = context.Categories.FirstOrDefault(c => c.Name == "Game systems").Id },
                new Category() { Name = "System units", Alias = "system-units", ParentId = context.Categories.FirstOrDefault(c => c.Name == "Game systems").Id },
                new Category() { Name = "Keyboards", Alias = "keyboards", ParentId = context.Categories.FirstOrDefault(c => c.Name == "Gaming Peripherals").Id },
                new Category() { Name = "Mouses", Alias = "mouses", ParentId = context.Categories.FirstOrDefault(c => c.Name == "Gaming Peripherals").Id },
                new Category() { Name = "Headphones", Alias = "headphones", ParentId = context.Categories.FirstOrDefault(c => c.Name == "Gaming Peripherals").Id }
            );

            context.SaveChanges();

            context.Products.AddOrUpdate(p => p.Name,
                new Product() { Name = "Asus TUF FX705DT", Alias = "asus-tuf-fx705dt", Description = "Cool", Stock = 500, Price = 750, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Notebooks").Id },
                new Product() { Name = "Gaming computer 01140", Alias = "gaming-computer-01140", Description = "Cool", Stock = 500, Price = 1000, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "System units").Id },
                new Product() { Name = "Marvo KG922GREEN", Alias = "marvo-kg922green", Description = "Cool", Stock = 500, Price = 55, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Keyboards").Id },
                new Product() { Name = "Razer RZ03-02521100-R3R1 Huntsman", Alias = "razer-rz003-02521100-r3r1-huntsman", Description = "Cool", Stock = 500, Price = 55, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Keyboards").Id },
                new Product() { Name = "Logitech M590", Alias = "logitech-m590", Description = "Cool", Stock = 500, Price = 35, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Mouses").Id }
            );*/

            var userManager = new AppUserManager(new UserStore<User>(context));
            var roleManager = new AppRoleManager(new RoleStore<Role>(context));

            var adminRole = new Role { Name = "Admin" };
            var userRole = new Role { Name = "User" };

            if (!roleManager.RoleExists(adminRole.Name))
            {
                roleManager.Create(adminRole);
            }

            if (!roleManager.RoleExists(userRole.Name))
            {
                roleManager.Create(userRole);
            }

            string userName = "admin@gmail.com";
            string password = "060389821Sas";

            User user = userManager.FindByName(userName);
            if (user == null)
            {
                userManager.Create(new User
                {
                    UserName = userName,
                    Email = "admin@gmail.com",
                    FirstName = "Alexandr",
                    LastName = "Rotaru"
                },
                    password);

                user = userManager.FindByName(userName);
            }

            // создаем claim 
            var firstName = new IdentityUserClaim { ClaimType = "FirstName", ClaimValue = user.FirstName };
            var lastName = new IdentityUserClaim { ClaimType = "LastName", ClaimValue = user.LastName };
            // добавляем claim пользователю
            user.Claims.Add(firstName);
            user.Claims.Add(lastName);
            // сохраняем изменения
            userManager.Update(user);

            if (!userManager.IsInRole(user.Id, adminRole.Name))
            {
                userManager.AddToRole(user.Id, adminRole.Name);
            }
        }
    }
}
