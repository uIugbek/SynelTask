using SynelTask.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Interfaces
{
    /// <summary>
    /// An Interface of Base Repository, that used to perform CRUD operations
    /// </summary>
    /// <typeparam name="T">An Entity that will be used for operations</typeparam>
    public interface IEntityRepository<T> where T : class, IEntityBase
    {
        /// <summary>
        /// Gets all data of the entity
        /// </summary>
        /// <returns>Returns Enumerable list of entity</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Asynchronously gets all data of the entity
        /// </summary>
        /// <returns>Returns Enumerable list of entity produced by Task</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets data of the entity as queryable
        /// </summary>
        /// <returns>Returns Queryable list of entity</returns>
        IQueryable<T> GetAllAsQueryable();

        /// <summary>
        /// Gets data of the entity as tracking queryable
        /// </summary>
        /// <returns>Returns Queryable list of entity</returns>
        IQueryable<T> GetAllAsQueryableTrack();

        /// <summary>
        /// Gets data of entity by given id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>Returns single entity</returns>
        T GetSingle(long id);

        /// <summary>
        /// Asynchronously gets data of entity by given id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>Returns single entity produced by Task</returns>
        Task<T> GetSingleAsync(long id);

        /// <summary>
        /// Adds new entity without saving changes
        /// </summary>
        /// <param name="entity">An instance of new entity</param>
        /// <returns>Returns added entity</returns>
        T Add(T entity);
        /// <summary>
        /// Updates an existing entity without saving changes
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        void Edit(T entity);
        /// <summary>
        /// Deletes an entity without saving changes
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        void Delete(T entity);
        /// <summary>
        /// Saving changes of entity
        /// </summary>
        /// <returns>Return true if saved successfully, else false</returns>
        bool Commit();
    }
}
