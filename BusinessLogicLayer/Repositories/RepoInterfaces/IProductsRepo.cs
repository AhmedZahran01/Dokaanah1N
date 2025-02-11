using Dokaanah.Models;

namespace Dokaanah.Repositories.RepoInterfaces
{
    public interface IProductsRepo : IGenericRepository<Product, int>
    { 
        List<Product> GetProductsByCategoryId(int CategoryId);

        List<Product> GetRandomProducts(int count);

        void AddProductToCart(int productId, int cartId);
         
        public IQueryable<Product> SearchByName(string Name , int categoryNumber);
          
    }
}
