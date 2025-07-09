using System.Security.Claims;
using BankAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        public BankController(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }


        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var empId = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);

                if (User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value == empId.ToString())
                {
                    var bank = _bankAccountRepository.Get(x => x.EmployeeId == empId);
                    return Ok(new { bank});    
                }

                return BadRequest("Anauthorize access");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
