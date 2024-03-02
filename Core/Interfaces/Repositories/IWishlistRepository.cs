using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IWishlistRepository<T> where T : EntityWithStrId
    {
        Task AddAsync(T entity);
    }
}
