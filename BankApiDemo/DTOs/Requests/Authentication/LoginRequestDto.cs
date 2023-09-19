using System.ComponentModel.DataAnnotations;

namespace BankApiDemo.DTOs.Requests.Authentication
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
