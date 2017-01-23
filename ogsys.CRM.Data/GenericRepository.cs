using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogsys.CRM.Data
{
    public class GenericRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class 
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository()
        {

        }

        public GenericRepository(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet
                    .AsNoTracking()
                    .ToList();
        }


        public TEntity GetById(int id)
        {
            return _dbSet
                    .Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            if (entityToDelete == null)
                return;
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context  != null) _context.Dispose();
        }
    }
}

