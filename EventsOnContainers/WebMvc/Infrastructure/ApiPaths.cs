using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public static class ApiPaths
    {
        //This is where we have all the url's for api's
        public static class Event
        {
            public static string GetAllEventTypes(string baseUri)
            {
                return $"{baseUri}EventTypes";
            }
            public static string GetAllEventCategories(string baseUri)
            {
                return $"{baseUri}EventCategories";
            }
            public static string GetAllEventAddresses(string baseUri)
            {
                return $"{baseUri}Addresses";
            }
            public static string GetAllEventOrganizers(string baseUri)
            {
                return $"{baseUri}Organizers";
            }
            //Take is like we are asking user, how many items they want
            public static string GetAllEventItems(string baseUri, int page, int take, int? catagory, int? type, int? address, int? organizer)
            {
             //   var filterQs = string.Empty;
                var catagoryQs = catagory.HasValue ? catagory.Value : -1;
                var typeQs = type.HasValue ? type.Value : -1;
                var addressQs = address.HasValue ? address.Value : -1;
                var organizerQs = organizer.HasValue ? organizer.Value : -1;
                var filterQs = $"/type/{typeQs}/catagory/{catagoryQs}/address/{addressQs}/organizer/{organizerQs}";
               /* if (catagory.HasValue || type.HasValue || address.HasValue || organizer.HasValue)
                {
                    var catagoryQs = (catagory.HasValue) ? catagory.Value : -1;
                    var typeQs = (type.HasValue) ? type.Value : -1;
                    var addressQs = (address.HasValue) ? address.Value : -1;
                    var organizerQs = (organizer.HasValue) ? organizer.Value : -1;
                    filterQs = $"/type/{typeQs}/catagory/{catagoryQs}/address/{addressQs}/organizer/{organizerQs}";
                } */
                return $"{baseUri}events{filterQs}?pageIndex={page}&pageSize={take}";
            }
        }
        public static class Basket
        {
            //baseUri is which we added in the dockercompose.yml which is http://cart
            public static string GetBasket(String baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(String baseUri)
            {
                //The data is comming from the body, thats why there is no uri
                return baseUri;
            }

            public static string CleanBasket(String baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }
    }
}
