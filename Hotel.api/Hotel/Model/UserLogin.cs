using System.Text.Json.Serialization;

namespace Hotel.Model
{
    public class UserLogin
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
