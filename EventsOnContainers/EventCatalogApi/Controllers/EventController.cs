using EventCatalogApi.Data;
using EventCatalogApi.Domain;
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
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var items = await _context.EventItems.
                OrderBy(c => c.EventName)
                .Skip(pageIndex * pageSize).
                Take(pageSize).ToListAsync();
            items = ChangePictureUrl(items);
            return Ok(items);
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


            var events = await query

                    .OrderBy(c => c.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangePictureUrl(events);

            return Ok(events);
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


            var events = await query

                    .OrderBy(c => c.Id)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangePictureUrl(events);

            return Ok(events);
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
                var items = await _context.EventItems.Join(_context.Addresses.Where(x => x.City.Equals(city)), eventItem => eventItem.EventAddressId,
              address => address.Id, (eventItem, address) => new
              {

                  eventId = eventItem.Id,
                  address = eventItem.EventItemAddress,
                  eventName = eventItem.EventName,
                  description = eventItem.Description,
                  price = eventItem.Price,
                  eventImage = eventItem.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]),
                  startTime = eventItem.EventStartTime,
                  endTime = eventItem.EventEndTime,
                  typeId = eventItem.EventTypeId,
                  categoryId = eventItem.EventCatagoryId
              }).OrderBy(c => c.eventId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize).ToListAsync();
                return Ok(items);
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

        //Addresses Filter
        [HttpGet("[action]/{CoordinatorName}")]
        public async Task<IActionResult> Organizers(
            string CoordinatorName,
           [FromQuery] int pageIndex = 0,
           [FromQuery] int pageSize = 4)

        {
            if (CoordinatorName != null && CoordinatorName.Length != 0)
            {
                var items = await _context.EventItems.Join(_context.Organizers.Where(x => x.Coordinator.Equals(CoordinatorName)), eventItem => eventItem.EventOraganizerId,
              organizer => organizer.Id, (eventItem, oraganizer) => new
              {

                  eventId = eventItem.Id,
                  eventOrganizer = eventItem.EventItemOraganizer,
                  address = eventItem.EventItemAddress,
                  eventName = eventItem.EventName,
                  description = eventItem.Description,
                  price = eventItem.Price,
                  eventImage = eventItem.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]),
                  startTime = eventItem.EventStartTime,
                  endTime = eventItem.EventEndTime,
                  typeId = eventItem.EventTypeId,
                  categoryId = eventItem.EventCatagoryId
              }).OrderBy(c => c.eventId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize).ToListAsync();
                return Ok(items);
            }



            return Ok();
        }

        //*******************************************************************************************************************

        //Re-orders events by date - oldest to newest
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Dates()
        {
            var events = await _context.EventItems
                .OrderBy(d => d.EventStartTime.Date)
                .ToListAsync();
            events = ChangePictureUrl(events);

            return Ok(events);
        }

        //Sorts event by month
        [HttpGet]
        [Route("[action]/{month}")]
        public async Task<IActionResult> FilterByMonth(int? month)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (month.HasValue)
            {
                query = query.Where(e => e.EventStartTime.Month == month);
            }

            var events = await query
                .OrderBy(e => e.EventStartTime)
                .ToListAsync();
            events = ChangePictureUrl(events);

            return Ok(events);
        }

        //filters events by specific date
        [HttpGet]
        [Route("[action]/{day}-{month}-{year}")]
        public async Task<IActionResult> FilterByDate(int? day, int? month, int? year)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (day.HasValue && month.HasValue && year.HasValue)
            {
                query = query.Where(e => e.EventStartTime.Day == day)
                             .Where(e => e.EventStartTime.Month == month)
                             .Where(e => e.EventStartTime.Year == year);
            }

            var events = await query
                .ToListAsync();
            events = ChangePictureUrl(events);

            return Ok(events);
        }

        //*******************************************************************************************************************


        private List<EventItem> ChangePictureUrl(List<EventItem> items)
        {
            items.ForEach(item =>
                item.PictureUrl = item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]));
            return items;
        }

    }
}
