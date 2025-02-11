using Dokaanah.Models;

namespace Dokaanah.Repositories.RepoInterfaces
{
    public interface ICartProductRepo : IGenericRepository<Cart_Product,int>
    { 
        Cart_Product GetAll(int productId, int cartId);           
    }
}