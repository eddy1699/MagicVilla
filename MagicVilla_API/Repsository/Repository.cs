﻿using MagicVilla_API.Data;
using MagicVilla_API.Repsository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace MagicVilla_API.Repsository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();        
        }
        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await Insert();
        }
        public async Task Insert()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>>  filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if(!tracked)
            {
                query= query.AsNoTracking();
            }
            if(filter != null)
            {
                query= query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

       
        public Task Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
