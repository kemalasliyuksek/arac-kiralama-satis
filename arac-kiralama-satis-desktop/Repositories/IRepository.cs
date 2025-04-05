using System;
using System.Collections.Generic;

namespace arac_kiralama_satis_desktop.Repositories
{
    /// <summary>
    /// Generic repository interface for CRUD operations
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public interface IRepository<T, TKey> where T : class
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        List<T> GetAll();

        /// <summary>
        /// Gets entity by id
        /// </summary>
        T GetById(TKey id);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        TKey Add(T entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        void Delete(TKey id);

        /// <summary>
        /// Searches for entities
        /// </summary>
        List<T> Search(string searchText);
    }
}