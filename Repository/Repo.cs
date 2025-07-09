using BankAPI.Db;
using BankAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Repository
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _set;

        public Repo(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _set;
        }

        public T Get(Func<T,bool> func)
        {
            IQueryable<T> query = _set;
            return query.Where(func).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddEntity(T entity) 
        { 
            _set.Add(entity);
            SaveChanges();
        }

        public void DeleteEntity(T entity)
        {
            _set.Remove(entity);
            SaveChanges();
        }

        public void UpdateEntity(T entity)
        {
            _set.Update(entity);
            SaveChanges();
        }
    }
}
