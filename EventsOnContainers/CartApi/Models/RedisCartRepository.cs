using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public class RedisCartRepository : ICartRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        //Below we are injecting the connection for Redis, like we did for Dbcontext(We injected connection strings)
        public RedisCartRepository(ConnectionMultiplexer redis)//Injecting connection to the redisserver and storing that 
                                                               //in private variable _redis
        {
            _redis = redis;
            _database = redis.GetDatabase();//we are asking redis to give a database
        }
        public async Task<bool> DeleteCartAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<Cart> GetCartAsync(string CartId)
        {
            var data = await _database.StringGetAsync(CartId);
            if (data.IsNullOrEmpty)
                return null;
            return JsonConvert.DeserializeObject<Cart>(data);
        }

        public IEnumerable<string> GetUsers()
        {
            //Getting all the servers(datacentres) end points.
            var endpoint = _redis.GetEndPoints();

            //Getting the nearst server end point
            var server =  _redis.GetServer(endpoint.First());
            //Getting the Id's of the users(Keys are nothing but Id's of users)
            var data = server.Keys();
            //data? means, we are telling the condition that if the data is not null,iterate and get all the keys
            return data?.Select(k => k.ToString());
        }

        public async Task<Cart> UpdateCartAsync(Cart basket)
        {
            //In below line we are saying that,First let go make my changes to the cart by replacing the items in the cart
            var created = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!created)
                return null;
            //Below line gives the latest version of cart
            return await GetCartAsync(basket.BuyerId);
        }
    }
}
