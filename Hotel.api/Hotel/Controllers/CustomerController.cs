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
            return Ok(await _context.Customers
                .Include(c => c.UserLogin)
                .ToListAsync()); 
     
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .Include(c => c.UserLogin)
                .ToListAsync();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(CreateCustomerDto request)
        {
          
            if (!CpfValidator.IsValid(request.CpfCustomer))
                return BadRequest("Invalid Cpf!");

            request.CpfCustomer = CpfFormatter.Format(request.CpfCustomer);

            //Working for RG's without a verification digit
            if (!Int32.TryParse(request.RgCustomer, out int n))
                return BadRequest("Invalid Rg!");

            
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                CpfCustomer = request.CpfCustomer,
                RgCustomer = request.RgCustomer
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
            
            
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(CreateCustomerDto request)
        {
            var dbCustomer = await _context.Customers.FindAsync(request.Id);
            if (dbCustomer == null)
                return BadRequest("Customer not found");

            if (!CpfValidator.IsValid(request.CpfCustomer))
                return BadRequest("Invalid Cpf!");

            request.CpfCustomer = CpfFormatter.Format(request.CpfCustomer);

            if (!Int32.TryParse(request.RgCustomer, out int n))
                return BadRequest("Invalid Rg!");

            dbCustomer.FirstName = request.FirstName;
            dbCustomer.LastName = request.LastName;
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
