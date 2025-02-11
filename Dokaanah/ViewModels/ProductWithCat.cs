namespace Dokaanah.PresentationLayer.ViewModels
{
    public class ProductWithCat
    {
        public int PId { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }

        // Category Data
        public int CId { get; set; }
        public string? CatName { get; set; }
    }
}
