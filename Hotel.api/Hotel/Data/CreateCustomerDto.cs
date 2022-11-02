namespace Hotel.Data
{
    public class CreateCustomerDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = "Antonio";
        public string LastName { get; set; } = "Neto";
        public string CpfCustomer { get; set; } = "000.000.000-00";
        public string RgCustomer { get; set; } = "0000000";
    }
}
