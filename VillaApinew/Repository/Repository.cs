using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaApinew.Data;
using VillaApinew.Modal;
using VillaApinew.Repository.IRepository;

namespace VillaApinew.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> Dbset;

        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            this.Dbset = db.Set<T>();
        }
        public async Task Create(T entity)
        {
            await Dbset.AddAsync(entity);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>>?filter = null, bool tracked = true)
        {
            IQueryable<T> query = Dbset;
            if (tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> ?filter = null)
        {
            IQueryable<T> query = Dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            Dbset.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        
    }
}
