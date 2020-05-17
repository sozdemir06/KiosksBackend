using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
    {
        private readonly TContext context;
        public EfEntityRepositoryBase(TContext context)
        {
            this.context = context;
        }

        public TEntity Add(TEntity entity)
        {
            return context.Add(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
             return await context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                                ? await context.Set<TEntity>().ToListAsync()
                                : await context.Set<TEntity>().Where(expression).ToListAsync();


        }

        public bool SaveChanges()
        {
            return context.SaveChanges()>0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync()>0;
        }

        public TEntity Update(TEntity entity)
        {
            context.Update(entity);
            return entity;
        }
    }
}