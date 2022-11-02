using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Hotel.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CpfCustomer { get; set; }
        public string RgCustomer { get; set; }
        [JsonIgnore]
        public List<Address> AddressesCustomer { get; set; }
        [JsonIgnore]
        public List<Contacts> ContactsCustomer { get; set; }
        public UserLogin UserLogin { get; set; }

    }
}
