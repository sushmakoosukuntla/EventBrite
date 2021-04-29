using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public interface ICartRepository
    {
       Task<Cart> GetCartAsync(String CartId);
       Task<Cart> UpdateCartAsync(Cart basket);
       Task<bool> DeleteCartAsync(String id);
        IEnumerable<String> GetUsers();

    }
}
