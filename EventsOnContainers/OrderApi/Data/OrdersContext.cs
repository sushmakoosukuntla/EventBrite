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
        public DbSet<OrderItem> OrderItems { get; set; }

        //Entity framework provides a pipeline on how it is going to be doing things.
        //For example, here is where i want to create the DB
        //Here is what i want to create
        //Here is how I want to create
    }
}
