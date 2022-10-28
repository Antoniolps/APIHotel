using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Hotel
{
    public class Contacts
    {
        public Guid Guid { get; set; }  
        public string CellPhone { get; set; }
        public string Email { get; set; }

        
        public Contacts(string cellPhone, string email)
        {
            Guid = Guid.NewGuid();
            CellPhone = cellPhone;
            Email = email;  
        }

    }
}
