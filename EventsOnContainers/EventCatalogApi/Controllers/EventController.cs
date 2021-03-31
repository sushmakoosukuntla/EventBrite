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
        [HttpGet("[action]")]//action is nothing but another name for method.
        public async Task<IActionResult> Items([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 6)
        {
            //Anytime we need to query the database table, we need to do it through Entity framework.
            var items = await _context.EventItems.OrderBy(c => c.EventName)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            items = ChangePictureUrl(items);
            return Ok(items);
        }

        private List<EventItem> ChangePictureUrl(List<EventItem> items)
        {
            items.ForEach(item =>
                item.PictureUrl = item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _config["ExternalCatalogBaseUrl"]));
            return items;
        }
    }
}
