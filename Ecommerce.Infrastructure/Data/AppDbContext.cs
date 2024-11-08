﻿using Ecommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<LocalUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<LocalUser> LocalUser { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasKey(x => new { x.Id, x.Order_Id, x.Product_Id });

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.Quantity)
                .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<Categories>().HasData(
                 new Categories { Id = 1, Name = "Furniture", Description = "Household furniture and fittings" },
                 new Categories { Id = 2, Name = "Toys", Description = "Children's toys and games" },
                 new Categories { Id = 3, Name = "Sports Equipment", Description = "Equipment for various sports" }
);

            modelBuilder.Entity<LocalUser>().HasData(
                 new LocalUser { Id = "1", FirstName = "Mona", LastName = "Al-Taher", Address = "X" },
                 new LocalUser { Id = "2", FirstName = "Ali", LastName = "Saeed", Address = "Y" },
                 new LocalUser { Id = "3", FirstName = "Sara", LastName = "Hassan", Address = "Z" }
            );

            modelBuilder.Entity<Products>().HasData(
                new Products { Id = 1, Name = "Sofa", Price = 499.99m, Image = "sofa.jpg", Category_Id = 1 },
                new Products { Id = 2, Name = "Dining Table", Price = 299.99m, Image = "dining_table.jpg", Category_Id = 1 },
                new Products { Id = 3, Name = "Action Figure", Price = 14.99m, Image = "action_figure.jpg", Category_Id = 2 },
                new Products { Id = 4, Name = "Football", Price = 19.99m, Image = "football.jpg", Category_Id = 3 },
                new Products { Id = 5, Name = "Basketball", Price = 24.99m, Image = "basketball.jpg", Category_Id = 3 }
            );

            modelBuilder.Entity<Orders>().HasData(
                new Orders { Id = 1, OrderStatus = "Processing", OrderDate = new DateTime(2024, 1, 15), LocalUser_Id = "1" },
                new Orders { Id = 2, OrderStatus = "Delivered", OrderDate = new DateTime(2024, 1, 16), LocalUser_Id = "2" },
                new Orders { Id = 3, OrderStatus = "Cancelled", OrderDate = new DateTime(2024, 1, 17), LocalUser_Id = "3" }
            );

            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails { Id = 1, Order_Id = 1, Product_Id = 1, Price = 499.99m, Quantity = 1 },
                new OrderDetails { Id = 2, Order_Id = 1, Product_Id = 4, Price = 19.99m, Quantity = 1 },
                new OrderDetails { Id = 3, Order_Id = 2, Product_Id = 3, Price = 14.99m, Quantity = 2 },
                new OrderDetails { Id = 4, Order_Id = 3, Product_Id = 2, Price = 299.99m, Quantity = 1 },
                new OrderDetails { Id = 5, Order_Id = 3, Product_Id = 5, Price = 24.99m, Quantity = 1 }
            );


            //modelBuilder.Entity<Categories>().HasData(
            //    new Categories { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
            //    new Categories { Id = 2, Name = "Books", Description = "Books and literature" },
            //    new Categories { Id = 3, Name = "Clothing", Description = "Apparel and accessories" }
            //);

            //modelBuilder.Entity<LocalUser>().HasData(
            //     new LocalUser { Id = "1", FirstName = "Raghad", LastName = "Abu Hafeeza", Address = "A" },
            //     new LocalUser { Id = "2", FirstName = "Ahmad", LastName = "Rami", Address = "B" },
            //     new LocalUser { Id = "3", FirstName = "Sami", LastName = "Nedal", Address = "c" }
            //);

            //modelBuilder.Entity<Products>().HasData(
            //    new Products { Id = 1, Name = "Smartphone", Price = 299.99m, Image = "smartphone.jpg", Category_Id = 1 },
            //    new Products { Id = 2, Name = "Laptop", Price = 799.99m, Image = "laptop.jpg", Category_Id = 1 },
            //    new Products { Id = 3, Name = "Novel", Price = 19.99m, Image = "novel.jpg", Category_Id = 2 },
            //    new Products { Id = 4, Name = "T-Shirt", Price = 9.99m, Image = "tshirt.jpg", Category_Id = 3 },
            //    new Products { Id = 5, Name = "Jeans", Price = 49.99m, Image = "jeans.jpg", Category_Id = 3 }
            //);

            //modelBuilder.Entity<Orders>().HasData(
            //    new Orders { Id = 1, OrderStatus = "Pending", OrderDate = new DateTime(2023, 12, 11), LocalUser_Id = "1" },
            //    new Orders { Id = 2, OrderStatus = "Completed", OrderDate = new DateTime(2023, 12, 12), LocalUser_Id = "2" },
            //    new Orders { Id = 3, OrderStatus = "Shipped", OrderDate = new DateTime(2023, 12, 13), LocalUser_Id = "3" }
            //);

            //modelBuilder.Entity<OrderDetails>().HasData(
            //    new OrderDetails { Id = 1, Order_Id = 1, Product_Id = 1, Price = 299.99m, Quantity = 1 },
            //    new OrderDetails { Id = 2, Order_Id = 1, Product_Id = 4, Price = 9.99m, Quantity = 2 },
            //    new OrderDetails { Id = 3, Order_Id = 2, Product_Id = 3, Price = 19.99m, Quantity = 1 },
            //    new OrderDetails { Id = 4, Order_Id = 3, Product_Id = 2, Price = 799.99m, Quantity = 1 },
            //    new OrderDetails { Id = 5, Order_Id = 3, Product_Id = 5, Price = 9.99m, Quantity = 1 }
            //);


            base.OnModelCreating(modelBuilder);
        }

    }
}
