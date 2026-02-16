using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models;

namespace RealEstateAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var values = await _context.Customers.ToListAsync();
            return values;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var value = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return value;
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if(existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.CustomerType = customer.CustomerType;
                existingCustomer.RoomPreference = customer.RoomPreference;
                existingCustomer.Note = customer.Note;

                await _context.SaveChangesAsync();
            }
        }
    }
}