using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderApi.Data;
using OrderApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        public readonly OrdersContext _ordersContext;//global variable
        public readonly IConfiguration _config;//global variable
        public readonly ILogger<OrdersController> _logger;//global variable
        public OrdersController(OrdersContext orderscontext, ILogger<OrdersController> logger,
             IConfiguration config)
        {
            _ordersContext = orderscontext;
            _config = config;
            _logger = logger;
        }
        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrder(int id)
        {
            //In _orderscontext we are returning everything in the Orders table
            var item = await _ordersContext.Orders
                .Include(x => x.OrderItems) //Include means joining the orderItems table
                .SingleOrDefaultAsync(ci => ci.OrderId == id);//go get this matching order id.
            if(item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrders()
        {
            //In _orderscontext we rae querying the Orders table
            var orders = await _ordersContext.Orders.ToListAsync();
            return Ok(orders);
        }

        [Route("new")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.OrderStatus = OrderStatus.Preparing;
            order.OrderDate = DateTime.UtcNow;
            _logger.LogInformation("In create order");
            _logger.LogInformation("Order" + order.UserName);

            _ordersContext.Orders.Add(order);//Adding a new row in to orders table(in to the database _orderscontext)
            _ordersContext.OrderItems.AddRange(order.OrderItems); //******************************
            _logger.LogInformation("Order added to context");
            _logger.LogInformation("saving.....");

            try
            {
                //TODO Database might not have been created
                await _ordersContext.SaveChangesAsync();
                return Ok(new { order.OrderId });
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError("An error occured while saving ..." + ex.Message);
                return BadRequest();
            }
        }
    }
}
