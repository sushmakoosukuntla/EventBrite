using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class Event

    {
        //We are not making this class generic, because we know we get list of EventItems 
        //This class is the representation of PaginatedItemsViewModel under ViewModels in EventCatalogAPi Microservice       
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }//Total count of items in the backend
        public List<EventItem> Data { get; set; }
    }
}
