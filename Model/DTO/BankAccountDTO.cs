using System.ComponentModel.DataAnnotations;

namespace BankAPI.Model.DTO
{
    public class BankAccountDTO
    {
        [Required] 
        public int EmployeeId { get; set; }
        [Required] 
        public string BankName { get; set; }
        [Required] 
        public string AccountNumber { get; set; }
        [Required] 
        public decimal Balance { get; set; }
    }
}
