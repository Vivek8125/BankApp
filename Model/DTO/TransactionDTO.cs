using System.ComponentModel.DataAnnotations;

namespace BankAPI.Model.DTO
{
    public class TransactionDTO
    {
        [Required] 
        public int ReceiverId { get; set; }
        [Required] 
        public decimal Amount { get; set; }
    }
}
