using BankAPI.Model;
using BankAPI.Model.DTO;
using BankAPI.NotificationHub;
using BankAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;


namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IEmployeeRepository _employeeRepository; 
        private readonly IHubContext<Notify> _hubContext;

        public TransactionController(ITransactionRepository transactionRepository, IWalletRepository walletRepository , IEmployeeRepository employeeRepository , IHubContext<Notify> hubContext)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _employeeRepository = employeeRepository;
            _hubContext = hubContext;
        }


        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var empId = Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                var transactions = _transactionRepository.GetAll()
                    .Where(x => x.SenderId == empId || x.ReceiverId == empId)
                    .ToList();


                var employeeIds = transactions
                    .SelectMany(t => new[] { t.SenderId, t.ReceiverId })
                    .Distinct()
                    .ToList();

                var employees = _employeeRepository.GetAll()
                    .Where(e => employeeIds.Contains(e.EmployeeId))
                    .ToList();

                var getTransaction = transactions.Select(t => new
                {
                    transactionId = t.TransactionId,
                    senderName = GetEmployeeName(employees, t.SenderId),
                    receiverName = GetEmployeeName(employees, t.ReceiverId),
                    amount = t.Amount,
                    timestamp = t.Timestamp,
                    type = empId == t.SenderId ? "Debit" : "Credit",
                    senderId = t.SenderId
                });

                return Ok(new { getTransaction });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransactionDTO transactionDto)
        {

            try
            {
                var empId = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);
                if(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value == empId.ToString())
                {
                    if (transactionDto == null)
                    {
                        return BadRequest();
                    }
                    if (empId == transactionDto.ReceiverId)
                    {
                        return BadRequest("Sender and Reciver not be same ");
                    }

                    var senderWallet = _walletRepository.Get(x => x.EmployeeId == empId);
                    var receiverWallet = _walletRepository.Get(x => x.EmployeeId == transactionDto.ReceiverId);

                    if(receiverWallet == null)
                    {
                        await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage", $"Wallet not exist");
                        return BadRequest();
                    }

                    if (senderWallet.Balance < transactionDto.Amount)
                    {
                        await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage", $"Insufficient funds");
                        return BadRequest();
                    }

                    receiverWallet.Balance += transactionDto.Amount;
                    senderWallet.Balance -= transactionDto.Amount;
                    receiverWallet.UpdatedDate = DateTime.Now;
                    senderWallet.UpdatedDate = DateTime.Now;
                    _transactionRepository.AddEntity(new Model.Transaction { SenderId = empId, ReceiverId = transactionDto.ReceiverId, Amount = transactionDto.Amount, Timestamp = DateTime.Now });
                    _transactionRepository.SaveChanges();

                    await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage", $"Payment sent: {transactionDto.Amount}");
                    await _hubContext.Clients.User(transactionDto.ReceiverId.ToString()).SendAsync("RecievedMessage", $"Payment Recieved: {transactionDto.Amount}");
                    return Ok(new { empId , transactionDto.ReceiverId , transactionDto.Amount});
                }
                return BadRequest("Anauthorized Access");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        private string GetEmployeeName(IEnumerable<Employee> employees, int id)
        {
            var emp = employees.FirstOrDefault(e => e.EmployeeId == id);
            return emp != null ? $"{emp.FirstName} {emp.LastName}" : "Unknown";
        }


    }
}
