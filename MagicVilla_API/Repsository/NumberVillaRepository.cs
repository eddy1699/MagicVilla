using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repsository.IRepository;

namespace MagicVilla_API.Repsository
{
    public class NumberVillaRepository : Repository<NumberVilla>, INumberVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public NumberVillaRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public async Task<NumberVilla> Update(NumberVilla entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            _db.NumberVilla.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
