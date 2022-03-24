using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain.Common;
using Banking.Account.Query.Infrastructure.Persistence;

namespace Banking.Account.Query.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly MySqlDbContext _context;

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

