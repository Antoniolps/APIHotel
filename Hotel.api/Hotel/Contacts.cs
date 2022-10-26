using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Hotel
{
    public class Contacts
    {
        public int Id { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }

        
        public Contacts(int id, string cellPhone, string email)
        {
            Id = id;
            CellPhone = cellPhone;
            Email = email;  
        }

    }
}
