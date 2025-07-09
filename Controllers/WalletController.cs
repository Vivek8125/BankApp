using System.Security.Claims;
using BankAPI.Model;
using BankAPI.Model.DTO;
using BankAPI.NotificationHub;
using BankAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IHubContext<Notify> _hubContext;
        public WalletController(IWalletRepository walletRepository , IEmployeeRepository employeeRepository, IBankAccountRepository bankAccountRepository , IHubContext<Notify> hubContext)
        {
            _walletRepository = walletRepository;
            _employeeRepository = employeeRepository;
            _bankAccountRepository = bankAccountRepository;
            _hubContext = hubContext;
        }


        [HttpGet]
        public ActionResult GetById()
        {
            try
            {
                var empId = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);
                if (User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value == empId.ToString())
                {
                    var wallet = _walletRepository.Get(x => x.EmployeeId == empId);
                    return Ok(new { wallet });
                }
                return BadRequest("Anauthorize Access");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        public async Task<ActionResult> Put([FromBody] WalletDTO walletDto)
        {
            try
            {
                var empId = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);

                var wallet = _walletRepository.Get(x => x.EmployeeId == empId);
                var bankAccount = _bankAccountRepository.Get(x => x.EmployeeId == empId);
                var employee = _employeeRepository.Get(x => x.EmployeeId == empId);

                if (employee == null)
                {
                    return BadRequest("Employee not found");
                }

                if (bankAccount == null)
                {
                    return BadRequest("Bank account not found");
                }

                if (walletDto.Balance <= 0)
                {
                    return BadRequest("Invalid amount");
                }

                if (bankAccount.Balance < walletDto.Balance)
                {
                    await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage", "Insufficient funds in the bank account");
                    return BadRequest("Insufficient Balance");
                }

                if (wallet == null)
                {
                    wallet = new Wallet
                    {
                        EmployeeId = empId,
                        Balance = walletDto.Balance,
                        UpdatedDate = DateTime.Now
                    };
                    _walletRepository.AddEntity(wallet);
                }
                else
                {
                    wallet.Balance += walletDto.Balance;
                    wallet.UpdatedDate = DateTime.Now;
                }

                bankAccount.Balance -= walletDto.Balance;

                _walletRepository.SaveChanges();
                _bankAccountRepository.SaveChanges();

                await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage", $"Funds added: {walletDto.Balance}");

                return Ok(new { wallet });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
