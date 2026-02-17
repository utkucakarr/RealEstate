using Core.Entites;
using Core.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repositories
{
    public class CachedCustomerRepository : ICustomerRepository
    {
        private readonly ICustomerRepository _innerRepository;
        private readonly IMemoryCache _memoryCache;
        private const string CustomerCacheKey = "customers_all";

        public CachedCustomerRepository(ICustomerRepository innerRepository, IMemoryCache memoryCache)
        {
            _innerRepository = innerRepository;
            _memoryCache = memoryCache;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _innerRepository.AddCustomerAsync(customer);
            _memoryCache.Remove(CustomerCacheKey); // Cache'i temizle
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _innerRepository.DeleteCustomerAsync(id);
            _memoryCache.Remove(CustomerCacheKey); // Cache'i temizle
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _memoryCache.GetOrCreateAsync(
                CustomerCacheKey,
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                    return _innerRepository.GetAllCustomersAsync();
                }) ?? Enumerable.Empty<Customer>();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _innerRepository.GetCustomerByIdAsync(id);
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            await _innerRepository.UpdateCustomerAsync(id, customer);
            _memoryCache.Remove(CustomerCacheKey); // Cache'i temizle
        }
    }
}
