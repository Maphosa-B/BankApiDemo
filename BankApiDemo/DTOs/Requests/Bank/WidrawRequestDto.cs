using System.ComponentModel.DataAnnotations;

namespace BankApiDemo.DTOs.Requests.Bank
{
    public class WidrawRequestDto
    {
        [Required]
        public string AccountNumber { get; set; } = "";

        [Required]
        public decimal Amount { get; set; }
    }
}
