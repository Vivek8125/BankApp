using BankAPI.Model.DTO;
using BankAPI.Repository.IRepository;
using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IEmployeeRepository _employeesRepository;
        private readonly ServiceToken _serviceToken;

        public LoginController(IEmployeeRepository employeesRepository , ServiceToken serviceToken)
        {
            _employeesRepository = employeesRepository;
            _serviceToken = serviceToken;
        }
        

        [HttpPost]
        public ActionResult TokenGenerate([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }

            var result = _employeesRepository.GetAll().ToList().Find(x => x.Username == loginDto.username && x.Password == loginDto.password);

            if (result == null)
            {
                return BadRequest("Invalid User Login");
            }

            var token = _serviceToken.GenerateToken(result.Username, result.EmployeeId.ToString());
            return Ok(new { token});



        }

        
    }
}
