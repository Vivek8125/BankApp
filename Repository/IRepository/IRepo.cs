namespace BankAPI.Repository.IRepository
{
    public interface IRepo<T> where T : class 
    {
        public IEnumerable<T> GetAll();

        public T Get(Func<T,bool> func);

        public void AddEntity(T entity);
        public void DeleteEntity(T entity);
        public void UpdateEntity(T entity);
        public void SaveChanges();
    }
}
