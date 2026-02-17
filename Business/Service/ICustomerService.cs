using Business.Dtos;

namespace Business.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> AddAsync(CustomerDto customerDto);
        Task UpdateAsync(int id, CustomerDto customerDto);
        Task DeleteAsync(int id);
    }
}
