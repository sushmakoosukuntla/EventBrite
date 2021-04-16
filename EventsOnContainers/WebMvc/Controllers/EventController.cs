using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }

        //We gonna get all these below info (Index parameters from the user)
        public async Task<IActionResult> Index(int? page, int? catagoriesFilterApplied, int? typesFilterApplied, int?
            addressesFilterApplied, int? organizersFilterApplied)

        {
            
            var itemsOnPage = 10;
           //?? means if there is no page number is guven the default page number will be 0
            var _event = await _service.GetEventItemsAsync(page ?? 0, itemsOnPage, catagoriesFilterApplied,
                typesFilterApplied, addressesFilterApplied, organizersFilterApplied);   
            
            if(null == _event){
                var vm1 = new EventIndexViewModel
                {
                    CatagoriesFilterApplied = catagoriesFilterApplied ?? 0,
                    TypesFilterApplied = typesFilterApplied ?? 0,
                    AddressesFilterApplied = addressesFilterApplied ?? 0,
                    OrganizersFilterApplied = organizersFilterApplied ?? 0
                };
                return View(vm1);
            }
            var vm = new EventIndexViewModel
            {
                EventItems = _event.Data,
                Catagories = await _service.GetEventCatagoriesAsync(),
                Types= await _service.GetEventTypesAsync(),
                Addresses = await _service.GetEventAddressesAsync(),
                Organizsers = await _service.GetEventOrganizersAsync(),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = _event.PageSize,
                    TotalItems = _event.Count,
                    TotalPages = (int)Math.Ceiling((decimal)_event.Count/itemsOnPage)
                },
                CatagoriesFilterApplied = catagoriesFilterApplied ?? 0,
                TypesFilterApplied = typesFilterApplied ?? 0,
                AddressesFilterApplied = addressesFilterApplied ?? 0,
                OrganizersFilterApplied = organizersFilterApplied ?? 0
            };

            return View(vm);
        }

        //below [Authorize] mean When ever someone comes to this method, First it will authorize
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View(ViewData);
        }
    }
}