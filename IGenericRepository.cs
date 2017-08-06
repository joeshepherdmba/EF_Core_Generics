using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_Generics.Repos
{
    /// <summary>
    /// Generic Repository Interface modeled from: http://www.remondo.net/repository-pattern-example-csharp/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Update(T entity);
        void SaveChanges();

        void Rollback();

        Task<T> GetByIdAsync(object id);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
 