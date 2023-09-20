using Microsoft.AspNetCore.Identity;

namespace BankApiDemo.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string IdNumber { get; set; } = "";
        public DateTime  DateOfBirth { get; set; }
    } 
}
