
using BankAPI.Db;
using BankAPI.Model;
using BankAPI.Repository.IRepository;

namespace BankAPI.Repository
{
    public class BankAccountRepository : Repo<BankAccount> , IBankAccountRepository
    {
        public BankAccountRepository(AppDbContext context): base(context)
        {
            
        }
    }
}
