using BankAPI.Model;
using BankAPI.Repository.IRepository;

namespace BankAPI.Repository.IRepository
{
    public interface ITransactionRepository : IRepo<Transaction>
    {
    }
}
