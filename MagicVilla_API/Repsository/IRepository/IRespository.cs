namespace MagicVilla_API.Repsository.IRepository
{
    public interface IRespository<T> where T : class 
    {
        Task Create(T entity);
    }
    
}
