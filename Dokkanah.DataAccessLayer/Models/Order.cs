using Dokaanah.DataAccessLayer.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokaanah.Models
 {
    public class Order
    {
        public int         Id                    { get; set; }
        public DateTime   Date                   { get; set; }
        public int        Amount                 { get; set; }
        public float      TotalPrice             { get; set; }
        public string?    PhoneNumber            { get; set; }
        public string     Status                 { get; set; }   // Preparing, Shipped, Delivered, Cancelled
                                                 
                                                 
        [ForeignKey("Customer")]                 
        public string?     Customerid            { get; set; }

        public virtual Customer? Customer        { get; set; } = null!;
        public virtual ICollection<Product> GetProducts { get; set; } = new List<Product>();

       




    }
}
