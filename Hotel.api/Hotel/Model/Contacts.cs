using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace Hotel.Model
{
    public class Contacts
    {
        public Guid Id { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
