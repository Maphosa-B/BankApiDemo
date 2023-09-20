using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApiDemo.Models
{
    public class WidrawalLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public DateTime AddDate { get; set; }


        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
