using MagicVilla_API.Models;
namespace MagicVilla_API.Repsository.IRepository
{
    public interface IVillaRepository:IRepository<Villa>
    {
        Task<Villa> Update(Villa entity);
    }
}
