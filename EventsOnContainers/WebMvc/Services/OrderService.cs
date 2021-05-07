using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Models;
using WebMvc.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebMvc.Infrastructure;

namespace WebMvc.Services
{
    public class OrderService : IOrderService
    {

        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly ILogger _logger;
        public OrderService(IConfiguration config,
            IHttpContextAccessor httpContextAccesor,
            IHttpClient httpClient, ILoggerFactory logger)
        {
            _remoteServiceBaseUrl = $"{config["OrderUrl"]}/api/orders";
            _config = config;
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger.CreateLogger<OrderService>();
        }
        public async Task<int> CreateOrder(Order order)
        {
            var token = await GetUserTokenAsync();//getting the token

            var addNewOrderUri = ApiPaths.Order.AddNewOrder(_remoteServiceBaseUrl);
            _logger.LogDebug(" OrderUri " + addNewOrderUri);//calling AddNewOrder to get the API path


            var response = await _apiClient.PostAsync(addNewOrderUri, order, token);//then making a post call
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error creating order, try later.");
            }

            // response.EnsureSuccessStatusCode();
            var jsonString = response.Content.ReadAsStringAsync();

            jsonString.Wait();
            _logger.LogDebug("response " + jsonString);

            //dynamic mean its anonymous type or dynamic type.
            //JArray jsonArray = JArray.Parse(jsonString.Result);
            //dynamic data = JObject.Parse(jsonArray[0].ToString);
            dynamic data = JObject.Parse(jsonString.Result);
            string value = data.orderId;
            return Convert.ToInt32(value);
        }

        public async Task<Models.OrderModels.Order> GetOrder(string id)
        {
            var token = await GetUserTokenAsync(); //given the id, first it Gets the token
            //Then gets the API path exactly where to go make the call
            var getOrderUri = ApiPaths.Order.GetOrder(_remoteServiceBaseUrl, id);
            //Then actually makes the httpclient call GetStringAsync, because it is get method, we call getstringasync
            var dataString = await _apiClient.GetStringAsync(getOrderUri, token);
            _logger.LogInformation("DataString: " + dataString);//Then logging
            var response = JsonConvert.DeserializeObject<Order>(dataString);//then deserializing the data in to order

            return response;
        }

        public async Task<List<Models.OrderModels.Order>> GetOrders()
        {
            var token = await GetUserTokenAsync();
            var allOrdersUri = ApiPaths.Order.GetOrders(_remoteServiceBaseUrl);

            var dataString = await _apiClient.GetStringAsync(allOrdersUri, token);
            //Here we are deserializing in to a list of order, because we get all orders
            var response = JsonConvert.DeserializeObject<List<Order>>(dataString);

            return response;
        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
        }
    }
}
