using MagicVilla_API.Models;
namespace MagicVilla_API.Repsository.IRepository
{
    public interface INumberVillaRepository:IRepository<NumberVilla>
    {
        Task<NumberVilla> Update(NumberVilla entity);
    }
}
