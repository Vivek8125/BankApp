using System.ComponentModel.DataAnnotations;

namespace BankAPI.Model
{
    public class BankAccount
    {
        [Key] public int BankAccountId { get; set; }
        [Required] public int EmployeeId { get; set; }
        [Required] public string BankName { get; set; }
        [Required] public string AccountNumber { get; set; }
        [Required] public decimal Balance { get; set; }
    }
}
