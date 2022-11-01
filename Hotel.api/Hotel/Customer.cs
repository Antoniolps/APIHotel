using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Hotel
{
    public class Customer
    {
        public Guid Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string CpfCustomer { get; set; }
        public string RgCustomer { get; set; }
        public List<Address> AddressesCustomer { get; set; }  
        public List<Contacts> ContactsCustomer { get; set; }
        public UserLogin UserLogin { get; set; }

    }
}
