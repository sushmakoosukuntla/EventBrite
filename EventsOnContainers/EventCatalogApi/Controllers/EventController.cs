using EventCatalogApi.Data;
using EventCatalogApi.Domain;
using EventCatalogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        //There are multiple ways to accept the Input from the user
        //1. from uri- this we have did for piccontroller
        //2. from query- we are doing this now
        //3. from body
        //Async Runs in a separate thread, and once it is done, the thread returns the task.
        //We have the dependency here with the CatalogContext class(EntityFramework class).So we are injecting 
        public readonly EventContext _context;//global variable
        public readonly IConfiguration _config;//global variable
        public EventController(EventContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //*********************************************************************************

        //Getting all the Event items 
        [HttpGet("[action]")]//action is nothing but another name for method.
        public async Task<IActionResult> Events(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var eventItemsCount = _context.EventItems.LongCountAsync();
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var items = await _context.EventItems.
                OrderBy(c => c.EventStartTime.Date)
                .Skip(pageIndex * pageSize).
                Take(pageSize).ToListAsync();
            items = ChangePictureUrl(items);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = eventItemsCount.Result, 
                Data = items
            };
            return Ok(model);
        }

        //************************************************************************************************
        [HttpGet("[action]")] //Getting all the categories.
        public async Task<IActionResult> EventCategories()

        {
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var categories = await _context.CategoryTypes.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("[action]/{eventCategoryId}")] //Getting Filtered categories
                                                //By keeping Question mark beside valuetype(int), we are making valuetype as nullable.

        public async Task<IActionResult> EventCategories(int? eventCategoryId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 4)

        {
            //By default, the below line means selct * from catalog.
            var query = (IQueryable<EventItem>)_context.EventItems;
            //Anytime we need to query the database table, we need to do it through Entity framework.
            if (eventCategoryId.HasValue)

            {
                query = query.Where(c => c.EventCatagoryId == eventCategoryId);
            }

            var eventItemsCount = query.LongCountAsync();
            var events = await query

                    .OrderBy(c => c.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventItemsCount.Result,
                Data = events
            };
            return Ok(model);            
        }

        //*******************************************************************************************************
        [HttpGet("[action]")] //Getting all the types.
        public async Task<IActionResult> EventTypes()

        {
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var types = await _context.EventTypes.ToListAsync();
            return Ok(types);
        }

        [HttpGet("[action]/{eventTypeId}")] //Getting Filtered categories
        //By keeping Question mark beside valuetype(int), we are making valuetype as nullable.

        public async Task<IActionResult> EventTypes(int? eventTypeId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 4)

        {
            //By default, the below line means selct * from catalog.
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (eventTypeId.HasValue)

            {
                query = query.Where(c => c.EventTypeId == eventTypeId);
            }

            var eventItemsCount = query.LongCountAsync();
            var events = await query

                    .OrderBy(c => c.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventItemsCount.Result,
                Data = events
            };

            return Ok(model);
        }

        //*****************************************************************************************************

        //Get ADressess
        [HttpGet("[action]")]
        public async Task<IActionResult> Addresses()
        {
            var addresses = await _context.Addresses.ToListAsync();
            return Ok(addresses);
        }

        //Addresses Filter
        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> Addresses(
            string city,
           [FromQuery] int pageIndex = 0,
           [FromQuery] int pageSize = 4)

        {
            
            if (city != null && city.Length != 0)
            {
                var query = from eventItem in _context.EventItems
                            join address in _context.Addresses
                            on eventItem.EventAddressId equals address.Id
                            where address.City == city

                select new EventItem 
                {
                    Id = eventItem.Id,
                    EventItemAddress = eventItem.EventItemAddress,
                    EventName = eventItem.EventName,
                    Description = eventItem.Description,
                    Price = eventItem.Price,
                    PictureUrl = eventItem.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]),
                    EventStartTime = eventItem.EventStartTime,
                    EventEndTime = eventItem.EventEndTime,
                    EventTypeId = eventItem.EventTypeId,
                    EventCatagoryId = eventItem.EventCatagoryId


                };
                var eventItemsCount = query.LongCountAsync();
                var events = await query

                        .OrderBy(c => c.Id)
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                var model = new PaginatedItemsViewModel<EventItem>
                {
                    PageIndex = pageIndex,
                    PageSize = events.Count,
                    Count = eventItemsCount.Result,
                    Data = events
                };
                return Ok(model);
            }
            return Ok();
        }

        //**********************************************************************************************************


        //Get organizers
        [HttpGet("[action]")]
        public async Task<IActionResult> Organizers()
        {
            var organizers = await _context.Organizers.ToListAsync();
            return Ok(organizers);
        }

        //Organizer filter Filter
        [HttpGet("[action]/{CoordinatorName}")]
        public async Task<IActionResult> Organizers(
            string CoordinatorName,
           [FromQuery] int pageIndex = 0,
           [FromQuery] int pageSize = 4)

        {
            if (CoordinatorName != null && CoordinatorName.Length != 0)
            {
                var query = from eventItem in _context.EventItems
                            join coordinator in _context.Organizers
                            on eventItem.EventOraganizerId equals coordinator.Id
                            where coordinator.Coordinator == CoordinatorName

                            select new EventItem
                            {
                                Id = eventItem.Id,
                                EventItemAddress = eventItem.EventItemAddress,
                                EventName = eventItem.EventName,
                                Description = eventItem.Description,
                                Price = eventItem.Price,
                                PictureUrl = eventItem.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                                _config["ExternalCatalogBaseUrl"]),
                                EventStartTime = eventItem.EventStartTime,
                                EventEndTime = eventItem.EventEndTime,
                                EventTypeId = eventItem.EventTypeId,
                                EventCatagoryId = eventItem.EventCatagoryId,
                                EventItemOraganizer = eventItem.EventItemOraganizer
                            };
                var eventItemsCount = query.LongCountAsync();
                var events = await query

                        .OrderBy(c => c.Id)
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                var model = new PaginatedItemsViewModel<EventItem>
                {
                    PageIndex = pageIndex,
                    PageSize = events.Count,
                    Count = eventItemsCount.Result,
                    Data = events
                };
                return Ok(model);
            }
                return Ok();
        }

        //*******************************************************************************************************************
       
        //Sorts event by month
        [HttpGet]
        [Route("[action]/{month}")]
        public async Task<IActionResult> FilterByMonth(int? month,
            [FromQuery] int pageIndex = 0)
            //[FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (month.HasValue)
            {
                query = query.Where(e => e.EventStartTime.Month == month);
            }
            var eventsCount = query.LongCountAsync();

            var events = await query
                .OrderBy(e => e.EventStartTime)
                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventsCount.Result,
                Data = events
            };

            return Ok(model);
        }

        //filters events by specific date
        [HttpGet]
        [Route("[action]/{day}-{month}-{year}")]
        public async Task<IActionResult> FilterByDate(int? day, int? month, int? year,
            [FromQuery] int pageIndex = 0)
            //[FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (day.HasValue && month.HasValue && year.HasValue)
            {
                query = query.Where(e => e.EventStartTime.Day == day)
                             .Where(e => e.EventStartTime.Month == month)
                             .Where(e => e.EventStartTime.Year == year);
            }
            var eventsCount = query.LongCountAsync();

            var events = await query
                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventsCount.Result,
                Data = events
            };

            return Ok(model);
        }

        //*******************************************************************************************************************


        private List<EventItem> ChangePictureUrl(List<EventItem> items)
        {
            items.ForEach(item =>
                item.PictureUrl = item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]));
            return items;
        }

        //*****************************************************************************************************************
        //By keeping Question mark beside valuetype(int), we are making valuetype as nullable.
        //action is nothing but another name for method.
        [HttpGet("[action]/type/{eventTypeId}/catagory/{eventCatagoryId}/address/{eventAddressId}/organizer/{eventOrganizerId}")]
        public async Task<IActionResult> Events(
            int? eventTypeId,
            int? eventCatagoryId,
            int? eventAddressId,
            int? eventOrganizerId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            //By default, the ablove line means selct * from catalog.
            //Anytime we need to query the database table, we need to do it through Entity framework.
            if (eventTypeId > 0)
            {
                query = query.Where(c => c.EventTypeId == eventTypeId);
            }
            if (eventCatagoryId >0)
            {
                query = query.Where(c => c.EventCatagoryId == eventCatagoryId);
            }
            if (eventAddressId >0)
            {
                query = query.Where(c => c.EventAddressId == eventAddressId);
            }
            if (eventOrganizerId >0)
            {
                query = query.Where(c => c.EventOraganizerId == eventOrganizerId);
            }
            var eventItemsCount = query.LongCountAsync();
            var items = await query
                .OrderBy(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            items = ChangePictureUrl(items);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = eventItemsCount.Result,
                Data = items
            };
            return Ok(model);
        }

        //*****************************************************************************************************************

    


    }
}
