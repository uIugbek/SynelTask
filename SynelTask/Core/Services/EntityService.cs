using SynelTask.Core.Interfaces;
using SynelTask.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Services
{
    /// <summary>
    /// A Base Service that performs CRUD and other operations on entities
    /// </summary>
    /// <typeparam name="T">An Entity that will be used for operations</typeparam>
    public class EntityService<T> : IEntityService<T>
        where T : class, IEntityBase
    {
        private readonly EntityRepository<T> _repository;

        /// <summary>
        /// Initializes an instance of EntityRepository
        /// </summary>
        /// <param name="repository">An instance of EntityRepository</param>
        public EntityService(EntityRepository<T> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all data of the entity
        /// </summary>
        /// <returns>Returns Enumerable list of entity</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Asynchronously gets all data of the entity
        /// </summary>
        /// <returns>Returns Enumerable list of entity produced by Task</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Gets data of the entity as queryable
        /// </summary>
        /// <returns>Returns Queryable list of entity</returns>
        public virtual IQueryable<T> GetAllAsQueryable()
        {
            return _repository.GetAllAsQueryable();
        }

        /// <summary>
        /// Gets data of the entity as tracking queryable
        /// </summary>
        /// <returns>Returns Queryable list of entity</returns>
        public virtual IQueryable<T> GetAllAsQueryableTrack()
        {
            return _repository.GetAllAsQueryableTrack();
        }

        /// <summary>
        /// Gets an entity by given id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>Returns single entity</returns>
        public T GetSingle(long id)
        {
            return _repository.GetSingle(id);
        }

        /// <summary>
        /// Asynchronously gets an entity by given id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>Returns single entity produced by Task</returns>
        public async Task<T> GetSingleAsync(long id)
        {
            return await _repository.GetSingleAsync(id);
        }

        /// <summary>
        /// Creates new entity and saves changings
        /// </summary>
        /// <param name="entity">An instance of new entity</param>
        /// <returns>Returns created entity</returns>
        public virtual T Create(T entity)
        {
            var newEntity = _repository.Add(entity);
            _repository.Commit();
            return newEntity;
        }

        /// <summary>
        /// Updates an existing entity and saves changings
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        /// <returns>Return true if updates successfully, else false</returns>
        public virtual bool Update(T entity)
        {
            _repository.Edit(entity);
            return _repository.Commit();
        }

        /// <summary>
        /// Deletes an entity and saves changings
        /// </summary>
        /// <param name="entity">An instance of entity</param>
        /// <returns>Return true if deletes successfully, else false</returns>
        public virtual bool Delete(int id)
        {
            var employee = _repository.GetSingle(id);

            if (employee == null)
                return false;

            _repository.Delete(employee);
            return _repository.Commit();
        }
    }
}
