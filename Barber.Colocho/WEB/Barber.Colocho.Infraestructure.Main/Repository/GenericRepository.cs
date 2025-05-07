using Barber.Colocho.Infraestructure.Persistence.Context;
using Barber.Colocho.Infraestructure.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Barber.Colocho.Infraestructure.Main.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var set = await _context.Set<T>().AddAsync(entity);
            await SaveCommitAsync();
            return set.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveCommitAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = dbSet;
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                if (filter != null)
                {
                    var result = await orderBy(query).FirstOrDefaultAsync(filter);
                    return result!;
                }
                else
                {
                    var result = await orderBy(query).FirstOrDefaultAsync();
                    return result!;
                }
            }
            else
            {
                if (filter != null)
                {
                    var result = await query.FirstOrDefaultAsync(filter);
                    return result!;
                }
                else
                {
                    var result = await query.FirstOrDefaultAsync();
                    return result!;
                }
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = dbSet;

            if (include != null)
            {
                query = include(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveCommitAsync();
        }


        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await SaveCommitAsync();
        }

        private async Task<int> SaveCommitAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var saved = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return saved;
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();
                return 0;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return 0;
            }
        }


        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await SaveCommitAsync();
        }
    }
}
