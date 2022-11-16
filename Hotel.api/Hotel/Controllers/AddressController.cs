using Hotel.Model;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly DataContext _context;
        public AddressController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Address>>> Get(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound();

            var adresses = await _context.Address
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            return adresses;
        }

        [HttpPost]
        public async Task<ActionResult<List<Address>>> Create(CreateAddressDto request)
        {
            var customer = await _context.Customers.Where(c => c.Id == request.Id).ToListAsync();
            if(customer == null)
                return NotFound();
            
            var address = new Address
            {
                Street = request.Street,
                Number = request.Number,
                District = request.District,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                CustomerId = request.CustomerId
            };

            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return await Get(request.CustomerId);
        }

        [HttpPut]
        public async Task<ActionResult<List<Address>>> UpdateAddress(UpdateAddressDto request)
        {
            if (request == null)
                return BadRequest("Request cant be null");

            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
                return NotFound();

            var addresses = await _context.Address
                .Where(a => a.CustomerId == request.CustomerId)
                .ToListAsync();

            var address = addresses.Find(a => a.Id == request.Id);
            if (address == null)
                return NotFound();

            address.Street = request.Street;
            address.Number = request.Number;
            address.District = request.District;
            address.City = request.City;
            address.State = request.State;
            address.PostalCode = request.PostalCode;
            address.CustomerId = request.CustomerId;

            await _context.SaveChangesAsync();

            return await Get(request.CustomerId);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Address>>> Delete(Guid id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address == null)
                return NotFound();

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return await Get(address.CustomerId);
        }
    }
}
