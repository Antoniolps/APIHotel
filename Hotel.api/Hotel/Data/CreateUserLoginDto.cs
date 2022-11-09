using Hotel.Model;
using System.Text.Json.Serialization;

namespace Hotel.Data
{
    public class CreateUserLoginDto
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public string UserName { get; set; } = "email@email.com";
        public string Password { get; set; } = "1234";
        public Guid CustomerId { get; set; } = Guid.NewGuid();   
    }
}
