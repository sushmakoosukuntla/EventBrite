using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventService : IEventService
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;

        public EventService(IConfiguration config, IHttpClient client)
        {
            
            _baseUrl = $"{config["EventUrl"]}/api/event/";
            _client = client;
        }
        public async Task<IEnumerable<SelectListItem>> GetEventCatagoriesAsync()
        {
            var eventCatagoryUri = ApiPaths.Event.GetAllEventCategories(_baseUrl);
            var dataString = await _client.GetStringAsync(eventCatagoryUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            var catagories = JArray.Parse(dataString);
            foreach (var catagory in catagories)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = catagory.Value<string>("id"),
                        Text = catagory.Value<string>("category")
                    });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventTypesAsync()
        {
            var eventTypeUri = ApiPaths.Event.GetAllEventTypes(_baseUrl);
            var dataString = await _client.GetStringAsync(eventTypeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    });
            }
            return items;
        }

        public async Task<Event> GetEventItemsAsync(int page, int size, int? catagory, int? type, int? address, int? organizer)
        {
            var EventItemsUri = ApiPaths.Event.GetAllEventItems(_baseUrl, page, size, catagory, type, address, organizer);
            var dataString = await _client.GetStringAsync(EventItemsUri);
            //Now we need to convert the string in to a paginated model.
            //In this service paginated is nothing but catalog class
            //Serializattion is converting the object in to a string(Json)
            //Deserialization is converting Json (String) in to an object
            return JsonConvert.DeserializeObject<Event>(dataString);
        }

        public async Task<IEnumerable<SelectListItem>> GetEventAddressesAsync()
        {
            var eventAddressUri = ApiPaths.Event.GetAllEventAddresses(_baseUrl);
            var dataString = await _client.GetStringAsync(eventAddressUri);
            var items = new List<SelectListItem>
            { 
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            HashSet<string> addresses1 = new HashSet<string>();           
            var addresses= JArray.Parse(dataString);
            foreach (var address in addresses)
            {
                addresses1.Add(address.Value<string>("city"));
                items.Add(
                    new SelectListItem
                    {
                        Value = address.Value<string>("id"),                        
                        Text = address.Value<string>("city")
                    });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventOrganizersAsync()
        {
            var eventOrganizerUri = ApiPaths.Event.GetAllEventOrganizers(_baseUrl);
            var dataString = await _client.GetStringAsync(eventOrganizerUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            var organizers = JArray.Parse(dataString);
            foreach (var organizer in organizers)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = organizer.Value<string>("id"),
                        Text = organizer.Value<string>("coordinator")
                    });
            }
            return items;
        }

    }
}
