using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Abstracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        T Find(object id);
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);

        IEnumerable<T> AddRange(List<T> entities);
        void DeleteRange(List<T> entities);

        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindBy(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = ""
         );
    }
}
