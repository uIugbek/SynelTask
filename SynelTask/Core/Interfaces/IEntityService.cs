using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Interfaces
{
    /// <summary>
    /// An Interface of Base Service for Entities
    /// </summary>
    /// <typeparam name="T">An Entity that will be used for operations</typeparam>
    public interface IEntityService<T> where T : class, IEntityBase
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
        /// Creates new entity and saves changings
        /// </summary>
        /// <param name="entity">An instance of new entity</param>
        /// <returns>Returns created entity</returns>
        T Create(T entity);

        /// <summary>
        /// Updates an existing entity and saves changings
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        /// <returns>Return true if updates successfully, else false</returns>
        bool Update(T entity);

        /// <summary>
        /// Deletes an entity and saves changings
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        /// <returns>Return true if deletes successfully, else false</returns>
        bool Delete(int id);
    }
}
