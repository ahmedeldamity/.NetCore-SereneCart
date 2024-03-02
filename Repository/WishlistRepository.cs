using Core.Entities;
using Core.Interfaces.Repositories;
using Repository.Data;

namespace Repository
{
    public class WishlistRepository<T>: IWishlistRepository<T> where T : EntityWithStrId
    {
        private readonly StoreContext _storeContext;

        public WishlistRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task AddAsync(T entity)
        {
            await _storeContext.Set<T>().AddAsync(entity);
        }
    }
}
