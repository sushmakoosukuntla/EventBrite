using EventCatalogApi.Domain;
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

        private static IEnumerable<EventOrganizer> GetEventOrganizers()
        {
            return new List<EventOrganizer>
            {
                new EventOrganizer
                {
                    Coordinator = "James", Title = "Eat, Sing and Dance"
                },

                new EventOrganizer
                {
                    Coordinator = "Sushma", Title ="Gain Knowledge"
                },

                new EventOrganizer
                {
                    Coordinator = "Rudra", Title = "Socialize"

                },

                new EventOrganizer
                {
                    Coordinator ="Kiran", Title = "Our Community Party"
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
                    Type ="Food festivals"
                },
                new EventType
                {
                    Type ="Music Festival"
                },
                new EventType
                {
                    Type = "Webinars"
                },
                new EventType
                {
                    Type ="Classes"
                },
                new EventType
                {
                    Type ="Interactive Performances"
                },
                new EventType
                {
                    Type ="Summits"
                },
                new EventType
                {
                    Type ="Reunions"
                },
                new EventType
                {
                    Type ="Themed Parties"
                },
                new EventType
                {
                    Type ="Street parties"
                },
                new EventType
                {
                    Type ="Swap Shops"
                },
                new EventType
                {
                    Type ="Litter picking and more"
                },
            };
        }

        private static IEnumerable<EventCategory> GetEventCategories()
        {
            return new List<EventCategory>
            {
                new EventCategory
                {
                    Category = "Festivals"
                },
                new EventCategory
                {
                    Category = "Virtual Events"
                },
                new EventCategory
                {
                    Category = "Social Events"
                },
                new EventCategory
                {
                    Category = "Community events"
                }
            };
        }
    }
}
