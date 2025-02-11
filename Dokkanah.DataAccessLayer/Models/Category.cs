namespace Dokaanah.Models
{
    public class Category:BaseEntity
    {
         public string? Description { get; set; }
         public string? Icon { get; set; }

         public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
