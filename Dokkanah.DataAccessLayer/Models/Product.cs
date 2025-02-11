using System.ComponentModel.DataAnnotations.Schema;

namespace Dokaanah.Models
{
    public class Product:BaseEntity
    {
       
            
            public float         Price                  { get; set; }
            public string?       Description            { get; set; }
            public string?       ImgUrl                 { get; set; }
            public int?          Quantity               { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; } 
        public virtual Category? Category { get; set; } = null!;
         
        public virtual ICollection<Cart_Product> Cart_Products { get; set; } = new List<Cart_Product>();
         
    }
}
