namespace Hotel
{
    public class Customer
    {

        public int IdCustomer { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string CpfCustomer { get; set; }
        public string RgCustomer { get; set; }
        public Address AddressCustomer { get; set; }
        public Contacts ContactsCustomer { get; set; }

        public Customer(int idCustomer, string firstName, string lastName, string cpfCustomer, string rgCustomer, Address addressCustomer, Contacts contactsCustomer)
        {
            IdCustomer = idCustomer;
            FirstName = firstName;
            LastName = lastName;
            CpfCustomer = cpfCustomer;
            RgCustomer = rgCustomer;
            AddressCustomer = addressCustomer;
            ContactsCustomer = contactsCustomer;    
        }

    }
}
