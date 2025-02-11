using Dokaanah.Models;

namespace Dokaanah.Repositories.RepoInterfaces
{
    public interface IGenericRepository<T,v> where T : class 
    {
        IEnumerable<T> GetAll();

        T GetById(v id); 

        int Insert(T customer);
       
        int Update(T customer);
        
        int Delete(T customer);
            
    }
}
