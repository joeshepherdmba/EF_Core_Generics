using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 

namespace EF_Core_Generics.Repos
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        protected DbContext context;

        public GenericRepository(DbContext dataContext)
        {
            context = dataContext;
            DbSet = dataContext.Set<T>();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await this.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Rollback()
        {
            context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
    }
}