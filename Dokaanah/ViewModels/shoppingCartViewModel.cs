using Dokaanah.Models;

namespace Dokaanah.PresentationLayer.ViewModels
{
    public class shoppingCartViewModel
    {

        //public List<Product>? CartItems { get; set; }
        public List<ShoppingCartItem>? CartItems { get; set; }

        public float? TotalPrice { get; set; }

        public int? TotalQuantity { get; set; }
    }
    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}