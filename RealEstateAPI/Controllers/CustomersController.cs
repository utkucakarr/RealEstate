using Business.Dtos;
using Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        // Artık IMapper enjekte etmemize gerek yok, çünkü servis katmanı bu işi hallediyor.
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customerDtos = await _customerService.GetAllAsync();
            return Ok(customerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _customerService.GetByIdAsync(id);
            if (dto == null) return NotFound("Müşteri bulunamadı.");

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            // Business katmanı mapping ve SaveChanges işlemlerini hallediyor.
            await _customerService.AddAsync(customerDto);

            // Not: customerDto içerisinde Id dolmuş olmalı (MappingProfile konfigürasyonuna bağlı)
            return CreatedAtAction(nameof(GetById), new { id = customerDto.Id }, customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto customerDto)
        {
            // Güncelleme öncesi varlık kontrolü servis içinde de yapılabilir
            var existing = await _customerService.GetByIdAsync(id);
            if (existing == null) return NotFound("Güncellenecek müşteri bulunamadı.");

            await _customerService.UpdateAsync(id, customerDto);
            return NoContent(); // Genelde güncelleme sonrası 204 No Content dönülür
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _customerService.GetByIdAsync(id);
            if (existing == null) return NotFound("Silinecek müşteri bulunamadı.");

            await _customerService.DeleteAsync(id);
            return NoContent();
        }
    }
}