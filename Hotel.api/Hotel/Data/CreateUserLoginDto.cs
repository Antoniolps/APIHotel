using Hotel.Model;

namespace Hotel.Data
{
    public class CreateUserLoginDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = "email@email.com";
        public string Password { get; set; } = "1234";
        public Guid CustomerId { get; set; } = Guid.NewGuid();   
    }
}
