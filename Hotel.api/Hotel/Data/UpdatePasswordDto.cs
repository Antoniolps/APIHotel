namespace Hotel.Data
{
    public class UpdatePasswordDto
    {
        public string Password { get; set; } = "123124124";
        public string NewPassword { get; set; } = "t222";
        public string ConfirmPassWord { get; set; } = "t222";
        public Guid CustomerId { get; set; } = Guid.NewGuid();
    }
}
