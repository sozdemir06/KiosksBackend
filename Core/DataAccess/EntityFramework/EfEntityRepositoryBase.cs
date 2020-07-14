using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Core.DataAccess.Specifications;
using Core.Entities;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {




        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FirstOrDefaultAsync(expression);
            }

        }



        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var context = new TContext())
            {
                return expression == null
                           ? await context.Set<TEntity>().ToListAsync()
                           : await context.Set<TEntity>().Where(expression).ToListAsync();
            }



        }


        public async Task<TEntity> GetEntityWithSpecAsync(ISpecification<TEntity> spec)
        {
            using (var context = new TContext())
            {
                return await ApplySpecification(spec, context).FirstOrDefaultAsync();
            }

        }

        public async Task<List<TEntity>> ListEntityWithSpecAsync(ISpecification<TEntity> spec)
        {
            using(var context=new TContext())
            {
                 return await ApplySpecification(spec,context).ToListAsync();
            }
           
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
             using(var context=new TContext())
            {
                 return await ApplySpecification(spec,context).CountAsync();
            }
           
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec, TContext context)
        {

            return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>().AsQueryable(), spec);


        }

        public async Task<TEntity> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await SaveChangeAsync(context);
                return entity;
            }

        }

        public async Task<TEntity> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await SaveChangeAsync(context);
                return entity;
            }

        }

        public async Task Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await SaveChangeAsync(context);
            }

        }

        private async Task SaveChangeAsync(TContext context)
        {

            var result = await context.SaveChangesAsync() > 0;
            if (!result)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { DatabaseCrudProblem = "Veritabanı İşlem Hatası..." });
            }


        }

        public async Task<List<TEntity>> AddRange(List<TEntity> entities)
        {
            using(var context=new TContext())
            {
                var addedEntity = context.Set<TEntity>();
                addedEntity.AddRange(entities);
                await SaveChangeAsync(context);
                return entities;
            }
        }

        public async Task DeleteRange(List<TEntity> entities)
        {
           using(var context=new TContext())
            {
                var deleteEntity = context.Set<TEntity>();
                deleteEntity.RemoveRange(entities);
                await SaveChangeAsync(context);
            }
        }

        public async Task<List<TEntity>> UpdateRange(List<TEntity> entities)
        {
           using(var context=new TContext())
            {
                var addedEntity = context.Set<TEntity>();
                addedEntity.UpdateRange(entities);
                await SaveChangeAsync(context);
                return entities;
            }
        }
    }
}