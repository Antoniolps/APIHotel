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

        [HttpGet]
        public async Task<ActionResult<List<Contacts>>> Get(Guid customerId)
        {
            var contacts = await _context.Contacts
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            return contacts;
        }

        [HttpPost]
        public async Task<ActionResult<List<Contacts>>> Create(CreateContactsDto request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
                return NotFound();

            var contact = new Contacts
            {
                CellPhone = request.CellPhone,
                Email = request.Email,
                CustomerId = request.CustomerId
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return await Get(request.CustomerId);
        }

        [HttpPut]
        public async Task<ActionResult<List<Contacts>>> UpdateAddress(CreateContactsDto request)
        {

            var contacts = await _context.Contacts
                .Where(a => a.CustomerId == request.CustomerId)
                .ToListAsync();

            var contact = contacts.Find(a => a.Id == request.Id);

            if (contact == null)
                return NotFound();

            contact.CellPhone = request.CellPhone;
            contact.Email = request.Email;
            contact.CustomerId = request.CustomerId;

            await _context.SaveChangesAsync();

            return await Get(request.CustomerId);
        }

        [HttpDelete]
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

