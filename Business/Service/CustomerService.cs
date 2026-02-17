using AutoMapper;
using Business.Dtos;
using Core.Entites;
using Core.Repositories;

namespace Business.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> AddAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.AddCustomerAsync(customer);

            // Kaydedilen veriyi ID'si ile birlikte geri dönmek iyi bir pratiktir
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, CustomerDto customerDto)
        {
            // DTO'yu Entity'ye çeviriyoruz
            var customer = _mapper.Map<Customer>(customerDto);

            // Repository'deki metoda ID ve Entity'yi gönderiyoruz
            await _customerRepository.UpdateCustomerAsync(id, customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
