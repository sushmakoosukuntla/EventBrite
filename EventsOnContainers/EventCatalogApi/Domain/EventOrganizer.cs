using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Domain
{
    public class EventOrganizer
    {
        public int Id { get; set; }
        public string Coordinator { get; set; }        
        public string ContactNumber { get; set; }//Choosen type string because contact number has "-" and "+"
    }
}
