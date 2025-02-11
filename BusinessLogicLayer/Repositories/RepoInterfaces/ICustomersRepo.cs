using Dokaanah.Models;

namespace Dokaanah.Repositories.RepoInterfaces
{
    public interface ICustomersRepo : IGenericRepository<Customer, string>
    {
        Customer GetByUserByEmail(string? emailOfAccount); 
        //int UserUpdate(Customer customer); 

    }
}
