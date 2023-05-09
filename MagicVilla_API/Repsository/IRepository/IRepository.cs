using System.Linq.Expressions;

namespace MagicVilla_API.Repsository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        Task Create(T entity);
        Task<List<T>> GetAll(Expression <Func<T,bool>> ? filter = null );
        Task<List<T>> Get(Expression<Func<T, bool>>? filter = null,bool tracked = true);

        Task Remove(T entity);
        Task Insert();
    }
    
}
