using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.ViewModels
{

    /*We addedd this Model because, Not only we just send the Event Items data back, but also we gonna wrap that data with
    page data. Like WHich page# you belong to, What is the page size, and total count
    So in order to send this additional data (data from multiple models) we introduced view model.*/
    public class PaginatedItemsViewModel<T>
    {
        /*This is generic class, because, now we want to send the catalog items in paginated form, but tommorow
         we want to send the manufacturers in paginated form. In this case we are representing different types of 
        data in the page */

        public IEnumerable<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }//Total count of items in the backend
    }
}
