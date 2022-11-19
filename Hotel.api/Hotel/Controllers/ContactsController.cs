using Hotel.Model;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly DataContext _context;
        public ContactsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Contacts>>> Get(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound("Customer not found");

            var contacts = await _context.Contacts
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            return contacts;
        }

        [HttpPost("{customerId}")]
        public async Task<ActionResult<List<Contacts>>> Create(CreateContactsDto request, Guid customerId)
        {

            if (request == null)
                return BadRequest("Request cant be null");

            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound("Customer not found");

            var contacts = await _context.Contacts.ToListAsync();

            foreach(var contact1 in contacts)
            {
                if (contact1.Email.Equals(request.Email))
                    return BadRequest("Email is in use!");
            }

            var contact = new Contacts
            {
                CellPhone = request.CellPhone,
                Email = request.Email,
                CustomerId = customerId
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return await Get(customerId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Contacts>>> UpdateAddress(UpdateContactsDto request, Guid id)
        {
            if (request == null)
                return BadRequest("Request cant be null");

            var contact = _context.Contacts.Find(id);

            if (contact == null)
                return NotFound();

            if (contact.Email.Equals(request.Email))
                return BadRequest("Email already exists!");

            var user = _context.Users.Where(u => u.CustomerId == contact.CustomerId).First();

            contact.CellPhone = request.CellPhone;
            contact.Email = request.Email;
            if(user != null)
                user.UserName = request.Email;

            await _context.SaveChangesAsync();

            return await Get(contact.CustomerId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Contacts>>> Delete(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return await Get(contact.CustomerId);
        }
    }
}

