using System.Security.Claims;
using BankAPI.Model.DTO;
using BankAPI.NotificationHub;
using BankAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace BankAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHubContext<Notify> _hubContext;
        public EmployeeController(IEmployeeRepository employeeRepository , IHubContext<Notify> hubContext)
        {
            _employeeRepository = employeeRepository;
            _hubContext = hubContext;
        }



        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var empId = Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var Employees = _employeeRepository.GetAll().ToList().Where(x => x.EmployeeId != empId);
                return Ok(Employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Password password)
        {
            try
            {
                var empId = Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var employee = _employeeRepository.Get(x => x.EmployeeId == empId);
                if (employee == null || password.password == null)
                {
                    return BadRequest();
                }
                if(password.password == employee.Password)
                {
                    await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage", "Same Password !!");
                    return Ok();
                }
                employee.Password = password.password;
                _employeeRepository.SaveChanges();
                await _hubContext.Clients.User(empId.ToString()).SendAsync("RecievedMessage" , "Password Updated");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
