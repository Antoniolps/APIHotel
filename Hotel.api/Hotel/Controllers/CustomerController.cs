using Hotel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Caelum.Stella.CSharp.Validation;
using Caelum.Stella.CSharp.Format;
using System.Numerics;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CPFValidator CpfValidator = new CPFValidator();
        public CPFFormatter CpfFormatter = new CPFFormatter();

        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return Ok(await _context.Customers.ToListAsync()); 
     
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .ToListAsync();

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddCustomer(CreateCustomerDto request)
        {
            
            if (request == null)
                return BadRequest("Request cant be null");

            if (!CpfValidator.IsValid(request.CpfCustomer))
                return BadRequest("Invalid Cpf!");

            request.CpfCustomer = CpfFormatter.Format(request.CpfCustomer);

            var consult = await _context.Customers.ToListAsync();

            foreach(var client in consult)
            {
                if (request.CpfCustomer.Equals(client.CpfCustomer))
                    return BadRequest("Customer already exists!");
            }

           

            //Working for RG's without a verification digit
            if (!Int32.TryParse(request.RgCustomer, out int n))
                return BadRequest("Invalid Rg!");

            
            var customer = new Customer
            {
                Name = request.Name,
                CpfCustomer = request.CpfCustomer,
                RgCustomer = request.RgCustomer
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.FindAsync(customer.Id));
            
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Customer>>> UpdateCustome(UpdateCustomerDto request, Guid id)
        {
            var dbCustomer = await _context.Customers.FindAsync(id);
            if (dbCustomer == null)
                return BadRequest("Customer not found");

            if (!CpfValidator.IsValid(request.CpfCustomer))
                return BadRequest("Invalid Cpf!");

            request.CpfCustomer = CpfFormatter.Format(request.CpfCustomer);

            if (!Int32.TryParse(request.RgCustomer, out int n))
                return BadRequest("Invalid Rg!");

            dbCustomer.Name = request.Name;
            dbCustomer.CpfCustomer = request.CpfCustomer;
            dbCustomer.RgCustomer = request.RgCustomer;

            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(Guid id)
        {
            var dbCustomer = await _context.Customers.FindAsync(id);
            if (dbCustomer == null)
                return BadRequest("Customer not found");

            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }


    }
}
