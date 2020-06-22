using Services.Domain.Interfaces;
using Services.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Infra.Repository
{
    public class RepositoryGenericAsync<T> : IGenericAsync<T>, IDisposable where T : class
    {
        private readonly DbContextOptionsBuilder<ContextBase> _OptionsBuilder;

        public RepositoryGenericAsync()
        {
            this._OptionsBuilder = new DbContextOptionsBuilder<ContextBase>();
        }

        public async Task<int> AddAsync(T Entity)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                await database.Set<T>().AddAsync(Entity);
                return await database.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(T Entity)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                database.Set<T>().Remove(Entity);
                return await database.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityAsync(Guid id)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                return await database.Set<T>().FindAsync(id);
            }
        }

        public async Task<List<T>> ListAsync()
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                return await database.Set<T>().ToListAsync();
            }
        }

        public async Task<int> UpdateAsync(T Entity)
        {
            using (var database = new ContextBase(this._OptionsBuilder.Options))
            {
                database.Set<T>().Update(Entity);
                return await database.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Dispose(bool isDispose)
        {
            if (!isDispose) return;
            GC.SuppressFinalize(this);
        }

        ~RepositoryGenericAsync()
        {
            this.Dispose(false);
        }
    }

}