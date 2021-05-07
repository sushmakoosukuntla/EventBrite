using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.OrderModels
{
    public class Order
    {
        //Anything with in the square brackets are called attributes
        [BindNever]//Microservice getting orderId from client, so we are mentioning that the binding is never needed
        //BindNever means, we never gonna get the data from this field ,im only sending the data in to this field
        public int OrderId { get; set; }

        [BindNever]
        public DateTime OrderDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")] //mentioning the display format
        public decimal OrderTotal { get; set; }

        [Required] //when ever we bind data with models, we expect that columns to come back with that
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }

        [BindNever]
        public string UserName { get; set; }
        [BindNever]
        public string BuyerId { get; set; }
        public string StripeToken { get; set; }

        public OrderStatus OrderStatus { get; set; }

        // See the property initializer syntax below. This
        // initializes the compiler generated field for this
        // auto-implemented property.
        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();


        public string PaymentAuthCode { get; set; }
    }
    public enum OrderStatus
    {
        Preparing = 1,
        Shipped = 2,
        Delivered = 3
    }
}
