using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Data
{
    public class EventContext : DbContext
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
        public EventContext(DbContextOptions options) : base(options)
        {

        }

        //So now, i have to tell what tables need to be created
        //DBSet(Set is an another name for table)
        /*In the below line, we are saying that, I want the table that follows "EventType" blueprint
         * and i want to name the table as EventTypes and so on for other tables*/
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventCategory> CategoryTypes { get; set; }
        public DbSet<EventAddress> Addresses { get; set; }
        public DbSet<EventItem> EventItems { get; set; }
        public DbSet<EventOrganizer> Organizers { get; set; }

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

            modelBuilder.Entity<EventCategory>(e =>
            {
                //In the below line we are telling that Brand is required.
                e.Property(c => c.Category).IsRequired().HasMaxLength(100);
                e.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

            });//EventCatagory Table

            modelBuilder.Entity<EventType>(e =>
            {
                e.Property(t => t.Type).IsRequired().HasMaxLength(100);
                e.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            }); //EventTypeTable

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
                e.Property(i => i.EventName).IsRequired().HasMaxLength(100);
                e.Property(i => i.Price).IsRequired();
                //e.Property(i => i.Description).IsRequired().HasMaxLength(100);
                e.Property(i => i.EventStartTime).IsRequired();
                e.Property(i => i.EventEndTime).IsRequired();
                //e.Property(i => i.PictureUrl).IsRequired().HasMaxLength(100);
                e.HasOne(i => i.EventItemAddress).WithMany().HasForeignKey(i => i.EventAddressId);

                e.HasOne(i => i.EventItemCatagory).WithMany().HasForeignKey(i => i.EventCatagoryId);

                e.HasOne(i => i.EventItemType).WithMany().HasForeignKey(i => i.EventTypeId);

                e.HasOne(i => i.EventItemOraganizer).WithMany().HasForeignKey(i => i.EventOraganizerId);                 
            }); //EventItem Table

            modelBuilder.Entity<EventAddress>(e =>
            {
                e.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
                e.Property(a => a.StreetAddress).IsRequired().HasMaxLength(100);
                e.Property(a => a.City).IsRequired().HasMaxLength(50);
                e.Property(a => a.State).IsRequired().HasMaxLength(50);
                e.Property(a => a.ZipCode).IsRequired();
            }); //EventAddressTable

            modelBuilder.Entity<EventOrganizer>(e =>
            {
                e.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
                e.Property(o => o.Coordinator).IsRequired().HasMaxLength(100);
                e.Property(o => o.ContactNumber).IsRequired().HasMaxLength(50);
            }); //EventOrganizerTable
        }
    }
}
