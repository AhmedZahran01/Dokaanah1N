using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Dokaanah.Repositories.RepoClasses
{
    public class CategoriesRepo : GenericRepository<Category , int>, ICategoriesRepo
    {
         public CategoriesRepo(DokkanahDBContex c1ontex10):base(c1ontex10)
         {
         } 

        // Get Products From Special Category
        public List<Product> GetProductsForCategory(int categoryId)
         => dokkanahContext.Products.Where(c => c.Id == categoryId)
            .Select(p => p).ToList();
 
    }
}
