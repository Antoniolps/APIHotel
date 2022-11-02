using System.Text.Json.Serialization;

namespace Hotel.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

    }
}
