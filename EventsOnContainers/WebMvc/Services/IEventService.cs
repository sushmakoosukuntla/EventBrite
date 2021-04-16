using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface IEventService
    {
        Task<Event> GetEventItemsAsync(int page, int size, int? catagory, int? type, int? address, int? organizer);
        Task<IEnumerable<SelectListItem>> GetEventCatagoriesAsync();
        Task<IEnumerable<SelectListItem>> GetEventTypesAsync();
        Task<IEnumerable<SelectListItem>> GetEventAddressesAsync();
        Task<IEnumerable<SelectListItem>> GetEventOrganizersAsync();
    }
}
