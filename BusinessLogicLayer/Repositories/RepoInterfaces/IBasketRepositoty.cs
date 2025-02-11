using Dokaanah.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dokaanah.BusinessLogicLayer.Repositories.RepoInterfaces
{
    public interface IBasketRepositoty
    {

        Task<CustomerBasket?> GetBasketAsync(string basketid);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketid);

    }
}
