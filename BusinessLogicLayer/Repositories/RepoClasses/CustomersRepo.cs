using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.EntityFrameworkCore;
namespace Dokaanah.Repositories.RepoClasses
{
    public class CustomersRepo : GenericRepository<Customer, String>, ICustomersRepo
    {
        public CustomersRepo(DokkanahDBContex dokkanah2) : base(dokkanah2)
        {
        }

        public Customer GetByUserByEmail(string? emailOfAccount)
         => dokkanahContext.Customers.FirstOrDefault(c => c.Email == emailOfAccount);



        //public int UserUpdate(Customer customer)
        //{
        //    dokkanahContext.Customers.Update(customer);
        //    return dokkanahContext.SaveChanges();
        //}
    }
}
