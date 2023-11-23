using Ecommerce.Domain.RepositoryContracts;
using Ecommerce.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ECommerceContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(ECommerceContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<IQueryable<T>> AsQueryable(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = filter == null ? _db : _db.Where(filter);
            query = (skip != null) ? query.Skip(skip.Value) : (take != null) ? query.Take(take.Value) : query;

            return query;
        }
        public T Get(Expression<Func<T, bool>> filter) => _db.FirstOrDefault(filter);
        public T Find(string id) => _db.Find(id);
        public void Insert(T model) => _db.Add(model);
        public void InsertList(List<T> model) => _db.AddRange(model);
        public void Update(T model) => _db.Update(model);
        public void UpdateList(List<T> model) => _db.UpdateRange(model);
        public void Delete(T model) => _db.Remove(model);
        public void DeleteList(List<T> model) => _db.RemoveRange(model);
        public void SaveChanges() => _context.SaveChanges();

        #region Async
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter) => await _db.FirstOrDefaultAsync(filter);
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null)
        {
            List<T> list = filter == null ? await _db.ToListAsync() : await _db.Where(filter).ToListAsync();
            list = (skip != null) ? list.Skip(skip.Value).ToList() : (take != null) ? list.Take(take.Value).ToList() : list.ToList();

            return list;
        }
        public async Task<T> FindAsync(string code) => await _db.FindAsync(code);
        public async Task InsertAsync(T model) => await _db.AddAsync(model);
        public async Task<T> InsertAsyncAndSave(T model)
        {
            await _db.AddAsync(model);
            await SaveChangesAsync();
            return model;
        }
        public async Task InsertListAsync(List<T> model) => await _db.AddRangeAsync(model);
        public async Task<T> UpdateAndSaveAsync(T model)
        {
            _db.Update(model);
            await SaveChangesAsync();
            return model;
        }

        #endregion
    }
}
