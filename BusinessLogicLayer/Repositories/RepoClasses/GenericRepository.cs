using Dokaanah.Models;
using Dokaanah.Repositories.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Dokaanah.Repositories.RepoClasses
{
    public class GenericRepository<T, V> : IGenericRepository<T, V> where T : class
    { 
        private protected readonly DokkanahDBContex dokkanahContext;
       
        public GenericRepository(DokkanahDBContex dokkanahcontext)  
              =>  this.dokkanahContext = dokkanahcontext;
         
        public IEnumerable<T> GetAll()
            => (from a in dokkanahContext.Set<T>() select a).ToList();
            
        public T GetById(V id)  
            =>  dokkanahContext.Find<T>(id);
             

        public int Insert(T order)
        { 
            dokkanahContext.Add(order); 
            return dokkanahContext.SaveChanges(); 
        }

        public int Update(T order)  
        {  
             dokkanahContext.Update(order);
             return dokkanahContext.SaveChanges();
        }
   
        public int Delete(T order)
        {  
            dokkanahContext.Remove(order);
            return dokkanahContext.SaveChanges();
        }
         

    }
}
