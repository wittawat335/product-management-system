using Ecommerce.Domain.RepositoryContracts;
using Ecommerce.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AngularEcommerceContext _context;

        public GenericRepository(AngularEcommerceContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<T>> AsQueryable(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null)
        {
            try
            {
                IQueryable<T> query = filter == null ? _context.Set<T>() : _context.Set<T>().Where(filter);

                if (skip != null)
                    query = query.Skip(skip.Value);

                if (take != null)
                    query = query.Take(take.Value);

                return query;
            }
            catch
            {
                throw;
            }
        }
        public T Get(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);
        }
        public T Find(Guid code)
        {
            return _context.Set<T>().Find(code);
        }
        public void Insert(T model)
        {
            _context.Set<T>().Add(model);
        }
        public void InsertList(List<T> model)
        {
            _context.Set<T>().AddRange(model);
        }
        public void Update(T model)
        {
            _context.Set<T>().Update(model);
        }
        public void UpdateList(List<T> model)
        {
            _context.Set<T>().UpdateRange(model);
        }
        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }
        public void DeleteList(List<T> model)
        {
            _context.Set<T>().RemoveRange(model);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region Async
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null)
        {
            try
            {
                List<T> list = filter == null ? await _context.Set<T>().ToListAsync() : await _context.Set<T>().Where(filter).ToListAsync();

                if (skip != null)
                    list = list.Skip(skip.Value).ToList();

                if (take != null)
                    list = list.Take(take.Value).ToList();

                return list;
            }
            catch
            {
                throw;
            }
        }
        public async Task<T> FindAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public async Task<T> InsertAsyncAndSave(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await SaveChangesAsync();
            return model;
        }

        public async Task InsertListAsync(List<T> model)
        {
            await _context.Set<T>().AddRangeAsync(model);
        }

        public async Task<T> UpdateAndSaveAsync(T model)
        {
            _context.Set<T>().Update(model);
            await SaveChangesAsync();
            return model;
        }

        #endregion
    }
}
