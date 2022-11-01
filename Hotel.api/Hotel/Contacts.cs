using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Hotel
{
    public class Contacts
    {
        public Guid Id { get; set; }  
        public string CellPhone { get; set; }
        public string Email { get; set; }

    }
}
