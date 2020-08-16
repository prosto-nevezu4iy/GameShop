using Core.Domain;
using Core.Infrastructure.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Core.Infrastructure
{
    public class GameShopContext : IdentityDbContext<User>
    {
        public GameShopContext() : base("name=GameShopContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static GameShopContext Create()
        {
            return new GameShopContext();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ParameterGroup> Groups { get; set; }
        public virtual DbSet<ParameterSubGroup> SubGroups { get; set; }
        public virtual DbSet<ParameterValue> Values { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new CartConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new ParameterGroupConfiguration());
            modelBuilder.Configurations.Add(new ParameterSubGroupConfiguration());
            modelBuilder.Configurations.Add(new ParameterValueConfiguration());
            modelBuilder.Configurations.Add(new WishListConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}