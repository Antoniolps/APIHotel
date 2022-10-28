using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private static List<Customer> customers = new List<Customer>
        {
            new Customer {
                Guid = Guid.NewGuid(),
                FirstName = "Antonio",
                LastName = "Lopes",
                CpfCustomer = "075.585.354-76",
                RgCustomer = "4427322",
                AddressesCustomer = new List<Address>
                {
                    new("Rua Etelvina Macedo Mendonca", "Torre", "João Pessoa", "Paraíba", "58040-530")
                },
                ContactsCustomer = new List<Contacts>
                {
                    new Contacts("83 99812-4410", "antoniolps218@gmail.com")
                }
            },
            new Customer {
                Guid = Guid.NewGuid(),
                FirstName = "Giarlando",
                LastName = "Alustal",
                CpfCustomer = "026.390.154-76",
                RgCustomer = "*******",
                AddressesCustomer = new List<Address>
                {
                    new("Rua Coronel Antonio Pessoa", "Centro", "Bananeiras", "Paraíba", "58220-000")
                },
                ContactsCustomer = new List<Contacts>
                {
                    new("83 99113-7602", "giarlando@gmail.com")
                }
            }

        };

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return Ok(customers);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Customer>> Get(Guid guid)
        {
            var customer = customers.Find(c => c.Guid == guid);
            if (customer == null)
                return BadRequest("Customer not found");
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            customers.Add(customer);
            return Ok(customers);
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer request)
        {
            var customer = customers.Find(c => c.Guid == request.Guid);
            if (customer == null)
                return BadRequest("Customer not found");

            customer.AddressesCustomer = request.AddressesCustomer;
            customer.ContactsCustomer = request.ContactsCustomer;

            return Ok(customers);
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateAddress(Customer request)
        {
            var customer = customers.Find(c => c.Guid == request.Guid);
            if (customer == null)
                return BadRequest("Customer not found");

            var address = customer.AddressesCustomer.Find(a => a.Guid == request.AddressesCustomer[0].Guid);
            if (address == null)
                return BadRequest("Address not found");
            int indexAddress = customer.AddressesCustomer.IndexOf(address);

            customer.AddressesCustomer[indexAddress] = request.AddressesCustomer[0];
            return Ok(customers);
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateContact(Customer request)
        {
            var customer = customers.Find(c => c.Guid == request.Guid);
            if (customer == null)
                return BadRequest("Customer not found");

            var contact = customer.ContactsCustomer.Find(c => c.Guid == request.ContactsCustomer[0].Guid);
            if (contact == null)
                return BadRequest("Contact not found");
            int indexContact = customer.ContactsCustomer.IndexOf(contact);

            customer.ContactsCustomer[indexContact] = request.ContactsCustomer[0];
            return Ok(customers);
        }


        [HttpDelete]
        public async Task<ActionResult<Customer>> Delete(Guid guid)
        {
            var customer = customers.Find(c => c.Guid == guid);
            if (customer == null)
                return BadRequest("Customer not found");

            customers.Remove(customer);
            return Ok(customers);
        }


    }
}
