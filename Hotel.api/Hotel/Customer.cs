using System.Net.NetworkInformation;

namespace Hotel
{
    public class Customer
    {

        public Guid Guid { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string CpfCustomer { get; set; }
        public string RgCustomer { get; set; }
        public List<Address> AddressesCustomer { get; set; }  
        public List<Contacts> ContactsCustomer { get; set; }

        /*
        public Customer(string firstName, string lastName, string cpfCustomer, string rgCustomer, List<Address> addresses, List<Contacts> contacts)
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;    
            CpfCustomer = cpfCustomer;  
            RgCustomer = rgCustomer;    
            AddressesCustomer = addresses;
            ContactsCustomer = contacts;
        }
        */

    }
}
