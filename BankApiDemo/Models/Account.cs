using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApiDemo.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal AvailableBalance { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        [Required]
        public string AccountNumber { get; set; } = "";

        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }


        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; } = "";

      
        //Navigation Properties
        public AccountType AccountType { get; set; }
        public IdentityUser IdentityUser { get; set; }

    }
}
