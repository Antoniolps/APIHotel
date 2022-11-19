namespace Hotel.Data
{
    public class UpdateAddressDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Street { get; set; } = "Rua A";
        public int Number { get; set; } = 000;
        public string District { get; set; } = "New";
        public string City { get; set; } = "Future City";
        public string State { get; set; } = "Acre";
        public string PostalCode { get; set; } = "00000-000";
    }
}
