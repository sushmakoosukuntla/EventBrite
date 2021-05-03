using OrderApi.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; } //units are basically quantity
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public OrderItem(int prodcuctId, string productName, decimal unitPrice, string pictureUrl, int units = 1)
        {
            if(units <= 0)
            {
                throw new  OrderingDomainException("Invalid  Number Of Units");
            }
            ProductId = prodcuctId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Units = units;
            PictureUrl = pictureUrl;
        }

        public void SetPictureUri(string pictureUri)
        {
            if (!string.IsNullOrWhiteSpace(pictureUri))
            {
                PictureUrl = pictureUri;
            }
        }
    }
}
