using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SynelTask.Core.Data;
using SynelTask.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Repositories
{
    /// <summary>
    /// A Base Repository, that used to perform CRUD operations
    /// </summary>
    /// <typeparam name="T">An Entity that will be used for operations</typeparam>
    public class EntityRepository<T> : IEntityRepository<T>
           where T : class, IEntityBase
    {

        public readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes an instance of ApplicationDbContext
        /// </summary>
        /// <param name="context">An instance of ApplicationDbContext</param>
        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all data of the entity
        /// </summary>
        /// <returns>Returns Enumerable list of entity</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        /// <summary>
        /// Asynchronously gets all data of the entity
        /// </summary>
        /// <returns>Returns Enumerable list of entity produced by Task</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Gets data of the entity as queryable
        /// </summary>
        /// <returns>Returns Queryable list of entity</returns>
        public virtual IQueryable<T> GetAllAsQueryable()
        {
            return _context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Gets data of the entity as tracking queryable
        /// </summary>
        /// <returns>Returns Queryable list of entity</returns>
        public virtual IQueryable<T> GetAllAsQueryableTrack()
        {
            return _context.Set<T>().AsTracking();
        }

        /// <summary>
        /// Gets data of entity by given id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>Returns single entity</returns>
        public T GetSingle(long id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Asynchronously gets data of entity by given id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>Returns single entity produced by Task</returns>
        public async Task<T> GetSingleAsync(long id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Adds new entity without saving changes
        /// </summary>
        /// <param name="entity">An instance of new entity</param>
        /// <returns>Returns added entity</returns>
        public virtual T Add(T entity)
        {
            var entry = _context.Set<T>().Add(entity);
            entity.Id = entry.Entity.Id;
            return entry.Entity;
        }

        /// <summary>
        /// Updates an existing entity without saving changes
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        public virtual void Edit(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes an entity without saving changes
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Saving changes of entity
        /// </summary>
        /// <returns>Return true if saved successfully, else false</returns>
        public virtual bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
