namespace Hotel.Data
{
    public class UpdateContactsDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CellPhone { get; set; } = "00000-0000";
        public string Email { get; set; } = "email@email.com";
    }
}
