using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Enums;

namespace WebshopServer.Models
{
    public class Order
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public long ArticleId { get; set; }
        public Article Article { get; set; }
        public long BuyerId { get; set; }
        public User Buyer { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? DeliveryTime { get; set; }
        public double? Price { get; set; }
    }
}
