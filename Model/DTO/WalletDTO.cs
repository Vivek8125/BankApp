using System.ComponentModel.DataAnnotations;

namespace BankAPI.Model.DTO
{
    public class WalletDTO
    {

        [Required] 
        public decimal Balance { get; set; }
    }
}
