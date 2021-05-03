using Microsoft.EntityFrameworkCore;
using OrderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Data
{
    public class OrdersContext : DbContext
    {
        //Entity framework will help us define how the interaction between the c# and the database work.

        /* we have to remember where we are going to write the data, what you are going to write
         * and how you are going to write.*/

        //This class is the instructions for entity framework.
        //We have to tell our entity framework the context, like where it is stored
        //we can tell that via inject
        //Injection can be done only through constructor.

        //in this example we are injecting DbContextOptions, which tells you where a database is located
        //When the injection happens, We will get the instructions to the parameter "Options" 
        //and that parameter we have to pass to base class.

        //In below line where part is injected
        public OrdersContext(DbContextOptions options) : base(options)
        {

        }

        //So now, i have to tell what tables need to be created
        //DBSet(Set is an another name for table)
        /*In the below line, we are saying that, I want the table that follows "Order" blueprint
         * and i want to name the table as Order and OrderItems*/
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order> OrderItems { get; set; }

        //Entity framework provides a pipeline on how it is going to be doing things.
        //For example, here is where i want to create the DB
        //Here is what i want to create
        //Here is how I want to create
        //So, in below method, we are overiding how to create DB
        //OnModelCreating means, OnTableCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //We tell modelBuilder to how to construct our table.
            //We are telling that, hey modelBuilder, when you are creating Entity CatalogBrand ,Let me tell
            //you how i wanted you to create them.
            //So, here the Entity<EventCatagory> is expecting to send an Action delegate or in other
            //words just write a method, that doesnt return anything.
            //so when ever a delegate is required, we write it as a lambda statement
            //In DB world entity refers to the table.
            modelBuilder.Entity<Order>(e =>
            {
                e.Property(o => o.OrderId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.Property(o => o.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                //In the below line we are saying that an order item is associated with one order, that ha many orderItems
                e.HasOne(o => o.Order)
                    .WithMany()
                    .HasForeignKey(o => o.OrderId);// and we are picking orderId column as Foreign key

            });



        }
    }
}
