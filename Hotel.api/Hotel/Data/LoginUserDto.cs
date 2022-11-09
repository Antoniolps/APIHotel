using System.Text.Json.Serialization;

namespace Hotel.Data
{
    public class LoginUserDto
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = "email@email.com";
        public string Password { get; set; } = "1234";
        [JsonIgnore]
        public Guid CustomerId { get; set; } = Guid.NewGuid();
    }
}
