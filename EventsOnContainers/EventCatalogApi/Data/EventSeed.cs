using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Data
{
    //We have to tell the entityframework, to migrate domains(classes) to the database world
    //If we dont call migrate, the tables dont exist nor subsequent changes wont happen
    //this class gonna seed dummy data for us 
    //We are now telling, what should be seeded in to tables when they are created
    //I want this class to seed CatalogContext database
    //when somebody is gonna call this below Seed method they are gonna tell me where the database is.
    public class EventSeed
    {
        public static void Seed(EventContext eventContext)
        {
            /*In order to migrate, Entityframe work requires the translated version of catalog context, To do that
            View => otherwindows => Package Manager console, this is powershell window whhere we can run powershell
            commands nothing but c# commands.*/
            //Then type Add-Migration Initial(For Initial migration)
            //What does Migrate do?? If we dont migrate, the tables will not get created in the database.
            eventContext.Database.Migrate();

            //we are checking whether the database is created or not in the below line.
            //catalogContext.Database.EnsureCreated();
            //Below Line checks the database, if it has any rows
            if (!eventContext.CategoryTypes.Any()) //EventCategory Table
            {
                eventContext.CategoryTypes.AddRange(GetEventCategories());
                //Until We save changes, it will not commit or create table.
                eventContext.SaveChanges();                
            }

            if (!eventContext.EventTypes.Any()) //EventType Table
            {
                eventContext.EventTypes.AddRange(GetEventTypes());
                //Until We save changes, it will not commit or create table.
                eventContext.SaveChanges();
            }

            if (!eventContext.Address.Any()) //EventAddress Table
            {
                eventContext.Address.AddRange(GetAddress());
                //Until We save changes, it will not commit or create table.
                eventContext.SaveChanges();
            }

            if (!eventContext.Organizers.Any()) //EventOrganizer Table
            {
                eventContext.Organizers.AddRange(GetEventOrganizers());
                //Until We save changes, it will not commit or create table.
                eventContext.SaveChanges();
            }

            if (!eventContext.EventItems.Any()) //EventItem Table
            {
                eventContext.EventItems.AddRange(GetEvents());
                //Until We save changes, it will not commit or create table.
                eventContext.SaveChanges();
            }
        }

        private static IEnumerable<EventItem> GetEvents()
        {
            return new List<EventItem>
            {

           
            new EventItem { EventCatagoryId = 1, EventTypeId = 2, EventAddressId = 4, EventOraganizerId = 3, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Let's have our tummy full", EventName = "Eat and Eat", Price = 20.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/1" },
            new EventItem { EventCatagoryId = 2, EventTypeId = 2, EventAddressId = 1, EventOraganizerId = 2, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Let us Dance, Hurrayy", EventName = "Dance on floor", Price = 10.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/2" },
            new EventItem { EventCatagoryId = 7, EventTypeId = 1, EventAddressId = 1, EventOraganizerId = 3, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Let us meet, its long time", EventName = "Reunion", Price = 0.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/3" },
            new EventItem { EventCatagoryId = 4, EventTypeId = 1, EventAddressId = 2, EventOraganizerId = 4, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Full STack Development", EventName = "Full Stack Basics", Price = 15.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/4" },
            new EventItem { EventCatagoryId = 3, EventTypeId = 3, EventAddressId = 2, EventOraganizerId = 4, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Java Webinar", EventName = "Learn by hearing", Price = 15.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/5" },
            new EventItem { EventCatagoryId = 9, EventTypeId = 1, EventAddressId = 3, EventOraganizerId = 4, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "let us busy up the Street", EventName = "Dance on Street Roads", Price = 0.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/6" },
            new EventItem { EventCatagoryId = 5, EventTypeId = 1, EventAddressId = 5, EventOraganizerId = 1, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Let us meet, Interact and Learn", EventName = "Interaction", Price = 0.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/7" },
            new EventItem { EventCatagoryId = 6, EventTypeId = 3, EventAddressId = 3, EventOraganizerId = 4, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "let us together clim the Mountain", EventName = "Summit Hike", Price = 25.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/8" },
            new EventItem { EventCatagoryId = 11, EventTypeId = 2, EventAddressId = 4, EventOraganizerId = 1, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Let's be Clean and Healthy", EventName = "Clean and Green", Price = 15.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/9" },           
            new EventItem { EventCatagoryId = 8, EventTypeId = 2, EventAddressId = 5, EventOraganizerId = 1, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Let us save Elephants", EventName = "Elephant Themed Party", Price = 10.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/10" },
            new EventItem { EventCatagoryId = 10, EventTypeId = 2, EventAddressId = 4, EventOraganizerId = 2, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Instant shopping", EventName = "Swap shop", Price = 0.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/11" },
            new EventItem { EventCatagoryId = 7, EventTypeId = 2, EventAddressId = 3, EventOraganizerId = 3, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "School Friends are the best", EventName = "Friends reunion", Price = 10.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/12" },
            new EventItem { EventCatagoryId = 3, EventTypeId = 1, EventAddressId = 4, EventOraganizerId = 3, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Empowering women is important", EventName = "Women Empowerment", Price = 15.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/13" },
            new EventItem { EventCatagoryId = 8, EventTypeId = 2, EventAddressId = 5, EventOraganizerId = 1, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Lets meet befor you tie the knot", EventName = "Bachelorite party", Price = 15.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/14" },
            new EventItem { EventCatagoryId = 2, EventTypeId = 3, EventAddressId = 2, EventOraganizerId = 4, EventStartTime = DateTime.Now, EventEndTime = DateTime.Now.AddMinutes(60), Description = "Lord Ganesha is the Epitome", EventName = "Ganesh Festival", Price = 10.0f, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/Image/15" },
            
            };
        }

        private static IEnumerable<EventOrganizer> GetEventOrganizers()
        {
            return new List<EventOrganizer>
            {
                new EventOrganizer
                {
                    Coordinator = "James", ContactNumber = "+1 415-630-1231"
                },

                new EventOrganizer
                {
                    Coordinator = "Sushma", ContactNumber = "+1 418-645-1236"
                },

                new EventOrganizer
                {
                    Coordinator = "Rudra", ContactNumber = "+1 415-530-1244"

                },

                new EventOrganizer
                {
                    Coordinator ="Kiran", ContactNumber = "+1 415-638-1321"
                }               
            };
        }

        private static IEnumerable<EventAddress> GetAddress()
        {
            return new List<EventAddress>
            {
                new EventAddress
                {
                    City = " Woodinville", State = "WA ", ZipCode =98055  , StreetAddress = " 12345 SE 100th Eve"
                },
                new EventAddress
                {
                    City = " Redmond", State = "WA ", ZipCode =98011  , StreetAddress = " 12463 NE 109th Eve"
                },
                new EventAddress
                {
                    City = " Bellevue", State = "WA ", ZipCode =98076  , StreetAddress = " 12100 SE 201st way"
                },
                new EventAddress
                {
                    City = " Seattle", State = "WA ", ZipCode =98030  , StreetAddress = " 12302 SE 108th Eve"
                },
                new EventAddress
                {
                    City = " Renton", State = "WA ", ZipCode =98044  , StreetAddress = " 12903 SE 202nd way"
                },
                new EventAddress
                {
                    City = " Bellevue", State = "WA ", ZipCode =98046  , StreetAddress = " 12908 NE 102nd way"
                },
                new EventAddress
                {
                    City = " Sammamish", State = "WA ", ZipCode =98350  , StreetAddress = " 12803 SE 113th St"
                },
                new EventAddress
                {
                    City = " Seattle", State = "WA ", ZipCode =98121  , StreetAddress = " 12460 NE 106th way"
                }
            };
        } 

        private static IEnumerable<EventType> GetEventTypes()
        {
            return new List<EventType>
            {
                new EventType
                { 
                    Type = "Online"
                },
                new EventType
                {
                    Type = "In Person"
                },
                new EventType
                {
                    Type = "Online & In Person"
                },               
            };
        }

        private static IEnumerable<EventCategory> GetEventCategories()
        {
            return new List<EventCategory>
            {               
                new EventCategory
                {
                    Category ="Food festivals"
                },
                new EventCategory
                {
                    Category ="Music Festival"
                },
                new EventCategory
                {
                    Category = "Webinars"
                },
                new EventCategory
                {
                    Category ="Classes"
                },
                new EventCategory
                {
                    Category ="Interactive Performances"
                },
                new EventCategory
                {
                    Category ="Summits"
                },
                new EventCategory
                {
                    Category ="Reunions"
                },
                new EventCategory
                {
                    Category ="Themed Parties"
                },
                new EventCategory
                {
                    Category ="Street parties"
                },
                new EventCategory
                {
                    Category ="Swap Shops"
                },
                new EventCategory
                {
                    Category ="Litter picking and more"
                },
            };
        }
    }
}
