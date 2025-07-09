using BankAPI.Db;
using BankAPI.Model;
using BankAPI.Repository.IRepository;

namespace BankAPI.Repository
{
    public class WalletRepository : Repo<Wallet>, IWalletRepository
    {
        
        public WalletRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
