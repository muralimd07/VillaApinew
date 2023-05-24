using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;
using VillaApinew.Modal;

namespace VillaApinew.Repository.IRepository
{
    public interface IVillaRepository:IRepository<Villa>
    {
        //Task<List<Villa>> GetAll(Expression<Func<Villa,bool>> filter=null);
        //Task<Villa> Get(Expression<Func<Villa,bool>> filter = null,bool tracked=true);
        //Task Create(Villa entity);
        Task<Villa> Update(Villa entity);
        //Task Remove(Villa entity);
        //Task Save();
      
    }
}
