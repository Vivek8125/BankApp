using System.ComponentModel.DataAnnotations;

namespace BankAPI.Model
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now; 
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
