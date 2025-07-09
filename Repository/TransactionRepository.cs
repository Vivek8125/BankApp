
using BankAPI.Db;
using BankAPI.Model;
using BankAPI.Repository.IRepository;

namespace BankAPI.Repository
{
    public class TransactionRepository : Repo<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
