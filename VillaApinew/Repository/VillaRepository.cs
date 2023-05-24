﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaApinew.Data;
using VillaApinew.Modal;
using VillaApinew.Repository.IRepository;

namespace VillaApinew.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) 
        {
            this._db = db;
        }
        public async Task Create(Villa entity)
        {
            await _db.AddAsync(entity);
            await Save();
        }

        public async Task<List<Villa>> Get(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if (tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<List<Villa>> GetAll(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task Remove(Villa entity)
        {
            _db.Villas.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Villa entity)
        {
            _db.Villas.Update(entity);
            await Save();
        }
    }
}
