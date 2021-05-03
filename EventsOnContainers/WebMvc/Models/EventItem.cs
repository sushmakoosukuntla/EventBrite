using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventItem
    {
        // this is EventItem Model in WebMvc project
        /*so basically we made a copy of catalogItem.cs from productCatalogApi/domains, so thet way when we get
         back the data  from the microservice, the microservice data will represent this CatalogItem Type,
        so we know which columns should display where*/
        public int Id { get; set; }
        public string EventName { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public int EventTypeId { get; set; }
        public int EventCatagoryId { get; set; }
        public int EventAddressId { get; set; }
        public int EventOraganizerId { get; set; }
        public string EventItemType { get; set; }
        public string EventItemCatagory { get; set; }
        public string EventItemAddress { get; set; }
        public string EventItemOraganizer { get; set; }
    }
}
