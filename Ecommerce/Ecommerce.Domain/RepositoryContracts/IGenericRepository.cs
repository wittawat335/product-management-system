﻿using System.Linq.Expressions;

namespace Ecommerce.Domain.RepositoryContracts
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> filter);
        T Find(string id);
        void Insert(T model);
        void InsertList(List<T> model);
        void Update(T model);
        void UpdateList(List<T> model);
        void Delete(T model);
        void DeleteList(List<T> model);
        void SaveChanges();

        #region Async
        Task<IQueryable<T>> AsQueryable(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> FindAsync(string code);
        Task InsertAsync(T model);
        Task<T> InsertAsyncAndSave(T model);
        Task InsertListAsync(List<T> model);
        Task<T> UpdateAndSaveAsync(T model);
        Task SaveChangesAsync();

        #endregion
    }
}
