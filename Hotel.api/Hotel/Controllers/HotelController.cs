using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly DataContext Context;

        public HotelController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return Ok(await Context.Customers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            var customer = await Context.Customers.FindAsync(id);
            if (customer == null)
                return BadRequest("Customer not found");

            return Ok(await Context.Customers.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            Context.Customers.Add(customer);
            await Context.SaveChangesAsync();

            return Ok(await Context.Customers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer request)
        {
            var dbCustomer = await Context.Customers.FindAsync(request.Id);
            if (dbCustomer == null)
                return BadRequest("Customer not found");
                
            dbCustomer.AddressesCustomer = request.AddressesCustomer;
            dbCustomer.ContactsCustomer = request.ContactsCustomer;

            await Context.SaveChangesAsync();

            return Ok(await Context.Customers.ToListAsync());
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(Guid id)
        {
            var dbCustomer = await Context.Customers.FindAsync(id);
            if (dbCustomer == null)
                return BadRequest("Customer not found");

            Context.Customers.Remove(dbCustomer);
            await Context.SaveChangesAsync();

            return Ok(await Context.Customers.ToListAsync());
        }


    }
}
