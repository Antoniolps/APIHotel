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
                Id = 1,
                FirstName = "Antonio",
                LastName = "Lopes",
                CpfCustomer = "075.585.354-76",
                RgCustomer = "4427322",
                AddressCustomer = new Address(1, "Rua Etelvina Macedo Mendonca", "Torre", "João Pessoa", "Paraíba", "58040-530"),
                ContactsCustomer = new Contacts(1, "83 99812-4410", "antoniolps218@gmail.com")
            },
            new Customer {
                Id = 2,
                FirstName = "Giarlando",
                LastName = "Alustal",
                CpfCustomer = "026.390.154-76",
                RgCustomer = "*******",
                AddressCustomer = new Address(1,"Rua Coronel Antonio Pessoa", "Centro", "Bananeiras", "Paraíba", "58220-000"),
                ContactsCustomer = new Contacts(1, "83 99113-7602", "giarlando@gmail.com")
            }

        };

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return Ok(customers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = customers.Find(c => c.Id == id);
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
            var customer = customers.Find(c => c.Id == request.Id);
            if (customer == null)
                return BadRequest("Customer not found");

            customer.AddressCustomer = request.AddressCustomer;
            customer.ContactsCustomer = request.ContactsCustomer;

            return Ok(customers);
        }

        [HttpDelete]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
                return BadRequest("Customer not found");

            customers.Remove(customer);
            return Ok(customers);
        }


    }
}
