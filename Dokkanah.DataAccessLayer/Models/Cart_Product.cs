namespace Dokaanah.Models
{
    public class Cart_Product
    {

        public int Caid { get; set; }
        public int Prid { get; set; }
        public int? ProductItemsNumbers { get; set; }  //Product Items Numbers and every Product has Product Quantity
        public int? NumberConstant { get; set; }  //Product Items Numbers and every Product has Product Quantity


        public virtual Product? Pr { get; set; } = null!;   
        public virtual Cart? Ca { get; set; } = null!;
        
    }
}
