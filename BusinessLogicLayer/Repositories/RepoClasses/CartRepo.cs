using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Dokaanah.Repositories.RepoClasses
{
    public class CartRepo : GenericRepository<Cart, int> , ICartRepo
    {
        public CartRepo(DokkanahDBContex context):base(context)
        {   
        } 
    }
}
