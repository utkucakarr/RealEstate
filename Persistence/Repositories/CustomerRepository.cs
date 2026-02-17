using Core.Entites;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            // Sadece veriyi listeleyeceğimiz için AsNoTracking() eklemek 
            // EF Core'un takip mekanizmasını kapatarak performansı ve bellek kullanımını iyileştirir.
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            // Tek bir kaydı okurken de AsNoTracking() kullanmak iyidir.
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer != null)
            {
                // ÖNEMLİ: Tüm özellikleri tek tek elle atamak yerine SetValues kullanabiliriz.
                // Bu metot, gönderdiğiniz 'customer' nesnesindeki eşleşen tüm alanları 
                // otomatik olarak mevcut kayda kopyalar. Yeni bir alan eklediğinizde burayı güncellemeniz gerekmez.
                customer.Id = id;
                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);

                // Id'nin değişmediğinden emin olalım (Genelde DTO'dan 0 gelebilir)
                existingCustomer.Id = id;

                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCustomerAsync(int id)
        {
            // Kaydı bulup silmek en güvenli yoldur.
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}