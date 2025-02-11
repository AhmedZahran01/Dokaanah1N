using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dokaanah.DataAccessLayer.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public int? DeliveryMenthodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }

    }
}
