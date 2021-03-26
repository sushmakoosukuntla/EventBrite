using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Domain
{
    public class EventItem
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public float Price { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }

        /*Now i have Four columns EventTypeId, EventCatagoryId, EVentAddressId, EventOrganizerId
        We have to associate these four columns with catelogItem*/

        //We have to build the relationship

        public int EventTypeId { get; set; }
        public int EventCatagoryId { get; set; }
        public int EventAddressId { get; set; }
        public int EventOraganizerId { get; set; }

        /*so for int EventTypeId in EventItem table is the Foriegn key to EventType table.
        so we are adding the below property of type EventType to refer*/

        public EventType EventItemType { get; set; }

        /*so for int EventCatagoryId in EventItem table, is the Foriegn key to EventCategory table.
        so we are adding the below property of type EventCatagory to refer*/
        public EventCategory EventItemCatagory { get; set; }

        /*so for int EventAddressId in EventItem table, is the Foriegn key to EventAddress table.
        so we are adding the below property of type EventAddress to refer*/
        public EventAddress EventItemAddress { get; set; }

        /*so for int EventOrganizerId in EventItem table, is the Foriegn key to EventOrganizer table.
        so we are adding the below property of type EventOrganizer to refer*/
        public EventOrganizer EventItemOraganizer { get; set; }


    }
}
