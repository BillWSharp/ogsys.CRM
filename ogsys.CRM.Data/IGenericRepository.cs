using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ogsys.CRM.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);

        //void AddNote(TEntity)

        // TODO: possibly use this later, need to decide
        //IEnumberable<TEntity> GetAllInclude(Expression<Func<TEntity>, bool>> predicate);
    }
}
