﻿namespace BookMangementSystemApi.Repository
{
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SaveAsync();
    }

}
