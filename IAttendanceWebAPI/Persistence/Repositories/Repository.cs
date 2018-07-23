using IAttendanceWebAPI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TEntity> Entities;

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;

            query = query.Where(predicate);

            foreach (var property in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            return await query.SingleOrDefaultAsync();
        }

        public IQueryable<TEntity> Queryable(IQueryable<TEntity> query,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            foreach (var property in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);
            if (orderBy != null) return orderBy(query);
            return query;
        }

        public async Task<IEnumerable<TEntity>> Get(IQueryable<TEntity> query,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            foreach (var property in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);
            if (orderBy != null) return orderBy(query).ToList();

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null) query = query.Where(filter);

            foreach (var property in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached) Entities.Attach(entity);
            Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            if (DbContext.Entry(enumerable).State == EntityState.Detached) Entities.AddRange(enumerable);
            Entities.RemoveRange(enumerable);
        }

        public void Update(TEntity entity)
        {
            Entities.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> Completed()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}