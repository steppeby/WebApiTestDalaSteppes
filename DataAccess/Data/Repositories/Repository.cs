using DataAccess.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WebApiDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(WebApiDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        /// <summary>
        /// Add entity by database using T 
        /// </summary>
        /// <param name="entity"></param>
        public async void Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        /// <summary>
        /// Find database entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> Find(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);   
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public async void Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
