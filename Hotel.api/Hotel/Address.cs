namespace Hotel
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        
        public Address(int id, string street, string district, string city, string state, string postalCode)
        {
            Id = id;
            Street = street;
            District = district;    
            City = city;    
            State = state;
            PostalCode = postalCode;    
        }
        
 

    }
}
