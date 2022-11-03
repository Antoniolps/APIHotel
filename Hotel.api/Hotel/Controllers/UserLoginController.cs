using Hotel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly DataContext _context;
        public UserLoginController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        
        public async Task<ActionResult<UserLogin>> Get(Guid CustomerId)
        {
            var user = await _context.Users
                .Where(u => u.CustomerId == CustomerId)
                .ToListAsync();
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserLogin>> AddUser(CreateUserLoginDto request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
                return NotFound();

            var contacts = await _context.Contacts
                .Where(a => a.CustomerId == request.CustomerId)
                .ToListAsync();
            if (contacts == null)
                return BadRequest();

            request.UserName = contacts.First().Email;

            var newUser = new UserLogin
            {
                UserName = request.UserName,
                Password = request.Password,
                CustomerId = request.CustomerId
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPut]
        public async Task<ActionResult<UserLogin>> UpdatePassword(UpdatePasswordDto request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
                return NotFound("Customer not found!");

            var user = await _context.Users
                .Where(u => u.CustomerId == request.CustomerId)
                .ToListAsync();
            if (user == null)
                return NotFound("User not Found");

            if (request.Password == user[0].Password)
            {
                if (request.NewPassword == request.ConfirmPassWord)
                {
                    user[0].Password = request.NewPassword;
                    await _context.SaveChangesAsync();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Passwords must be the same");
                }
            }
            else
                return BadRequest("Incorrect current password");
            
        }

        [HttpDelete]
        public async Task<ActionResult<UserLogin>> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User was been removed");
        }
    }
}

