using System.ComponentModel.DataAnnotations.Schema;

namespace Dokaanah.Models
{
    public class Cart:BaseEntity
    {

        public bool? IsDeleted { get; set; } = true; 
     
        public virtual ICollection<Cart_Product> Cart_Products { get; set; } = new List<Cart_Product>();

        [ForeignKey("Customer")]
        public int? Custid { get; set; }
    }
}
