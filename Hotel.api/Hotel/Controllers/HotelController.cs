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
            new Customer (
              
                1,
                "Antonio",
                "Lopes",
                "075.585.354-76",
                "4427322",
                new Address("Rua Etelvina Macedo Mendonca", "Torre", "João Pessoa", "Paraíba", "58040-530"),
                new Contacts("83 99812-4410", "antoniolps218@gmail.com")
                
            )
        };

        [HttpGet]

        public async Task<ActionResult<List<Customer>>> Get()
        {
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            customers.Add(customer);
            return Ok(customers);
        }


    }
}
