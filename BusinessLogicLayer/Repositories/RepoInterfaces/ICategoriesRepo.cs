using Dokaanah.Models;

namespace Dokaanah.Repositories.RepoInterfaces
{
    public interface ICategoriesRepo : IGenericRepository<Category, int>
    {
 
        List<Product> GetProductsForCategory(int categoryId);
         
    }
}
