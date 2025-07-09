using BankAPI.Db;
using BankAPI.Model;
using BankAPI.Repository.IRepository;

namespace BankAPI.Repository
{
    public class EmployeeRepository : Repo<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context ) : base(context)
        {
        }
    }
}
