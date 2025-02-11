using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Dokaanah.Repositories.RepoClasses
{
    public class ProductsRepo : GenericRepository<Product, int>,  IProductsRepo
    {
        #region constroctor
 
        public ProductsRepo(DokkanahDBContex context):base(context) 
        {
         
        }
        #endregion
           
        public List<Product> GetRandomProducts(int count)
         => dokkanahContext.Products.OrderBy(p => Guid.NewGuid()).Take(count).ToList();
          
         
        public IQueryable<Product> SearchByName(string name, int categoryNumber)
          => dokkanahContext.Products.Where(p => p.Name.ToLower().Contains(name))
                      .Where(p => p.CategoryId == categoryNumber);

        public void AddProductToCart(int productId, int cartId)
        {
            var cartProduct = new Cart_Product
            {
                Prid = productId,
                Caid = cartId
            }; 
            dokkanahContext.Cart_Products.Add(cartProduct);
            dokkanahContext.SaveChanges();
        }

      
        public List<Product> GetProductsByCategoryId(int CategoryId)
         => dokkanahContext.Products.Where( p => p.CategoryId == CategoryId).ToList();
         

    }
}
