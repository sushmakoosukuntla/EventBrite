using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.CartModels;
using static System.Net.Mime.MediaTypeNames;

namespace WebMvc.Services
{
    public interface ICartService
    {
        //Application User is a token that is comming from the Identity user 
        Task<Cart> GetCart(Application User);
        Task AddItemToCart(Application User, CartItem product);
        Task<Cart> UpdateCart(Cart cart);//Deleting or adding a prodcut
        //Dictionary<String, int> quantities means for each product ID how much quantity you gonna increase
        Task<Cart> SetQuantities(Application User, Dictionary<String, int> quantities);
        Task ClearCart(Application User);
    }
}
