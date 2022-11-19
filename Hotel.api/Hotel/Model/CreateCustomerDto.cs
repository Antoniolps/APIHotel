using System.Text.Json.Serialization;

namespace Hotel.Model
{
    public class CreateCustomerDto
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Antonio Neto";
        public string CpfCustomer { get; set; } = "000.000.000-00";
        public string RgCustomer { get; set; } = "0000000";
    }
}
