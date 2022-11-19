using Hotel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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

        [HttpPost("register/{customerId}")]
        public async Task<ActionResult<UserLogin>> Register(CreateUserLoginDto request, Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound();

            var contacts = await _context.Contacts
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
            if (contacts == null)
                return BadRequest();

            request.UserName = contacts.First().Email;

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new UserLogin
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CustomerId = customerId
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);            
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserDto request)
        {
            var users = await _context.Users.Where(u => u.UserName == request.UserName).ToListAsync();
            var user = users[0];

            if(user.UserName != request.UserName)
                return BadRequest("User not Found!");

            if (!VerifyPasswordHash(request.Password, user))
                return BadRequest("Wrong Password!");

            var customer = await _context.Customers.FindAsync(user.CustomerId);

            if (customer == null)
                return BadRequest("Customer not exists");

            return Ok(customer);
        }

        [NonAction]
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [NonAction]
        public bool VerifyPasswordHash(string password, UserLogin user)
        {
            using(var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}

