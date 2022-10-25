using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Hotel
{
    public class Contacts
    {
        public string CellPhone { get; set; }
        public string Email { get; set; }

        
        public Contacts(string cellPhone, string email)
        {
            CellPhone = cellPhone;
            Email = email;  
        }

    }
}
