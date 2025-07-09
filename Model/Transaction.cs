using System.ComponentModel.DataAnnotations;

namespace BankAPI.Model
{
    public class Transaction
    {
        [Key] public int TransactionId { get; set; }
        [Required] public int SenderId { get; set; }
        [Required] public int ReceiverId { get; set; }
        [Required] public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
