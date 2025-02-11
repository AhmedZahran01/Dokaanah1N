using System.ComponentModel.DataAnnotations;

namespace Dokaanah.PresentationLayer.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

    }
}