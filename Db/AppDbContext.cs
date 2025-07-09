using BankAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
        new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Username = "john_doe", Password = "password1" },
        new Employee { EmployeeId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Username = "jane_smith", Password = "password2" },
        new Employee { EmployeeId = 3, FirstName = "Emily", LastName = "Johnson", Email = "emily.johnson@example.com", Username = "emily_johnson", Password = "password3" },
        new Employee { EmployeeId = 4, FirstName = "Michael", LastName = "Brown", Email = "michael.brown@example.com", Username = "michael_williams", Password = "password4" },
        new Employee { EmployeeId = 5, FirstName = "Sarah", LastName = "Davis", Email = "sarah.davis@example.com", Username = "sarah_brown", Password = "password5" },
        new Employee { EmployeeId = 6, FirstName = "David", LastName = "Miller", Email = "david.miller@example.com", Username = "david_jones", Password = "password6" },
        new Employee { EmployeeId = 7, FirstName = "Laura", LastName = "Wilson", Email = "laura.wilson@example.com", Username = "laura_garcia", Password = "password7" },
        new Employee { EmployeeId = 8, FirstName = "James", LastName = "Moore", Email = "james.moore@example.com", Username = "james_martinez", Password = "password8" },
        new Employee { EmployeeId = 9, FirstName = "Linda", LastName = "Taylor", Email = "linda.taylor@example.com", Username = "linda_hernandez", Password = "password9" },
        new Employee { EmployeeId = 10, FirstName = "Robert", LastName = "Anderson", Email = "robert.anderson@example.com", Username = "robert_lopez", Password = "password10" }
    );

            modelBuilder.Entity<BankAccount>().HasData(
        new BankAccount { BankAccountId = 1, EmployeeId = 1, BankName = "Bank A", AccountNumber = "1234567890", Balance = 1000 },
        new BankAccount { BankAccountId = 2, EmployeeId = 2, BankName = "Bank B", AccountNumber = "2345678901", Balance = 1500 },
        new BankAccount { BankAccountId = 3, EmployeeId = 3, BankName = "Bank C", AccountNumber = "3456789012", Balance = 2000 },
        new BankAccount { BankAccountId = 4, EmployeeId = 4, BankName = "Bank D", AccountNumber = "4567890123", Balance = 2500 },
        new BankAccount { BankAccountId = 5, EmployeeId = 5, BankName = "Bank E", AccountNumber = "5678901234", Balance = 3000 },
        new BankAccount { BankAccountId = 6, EmployeeId = 6, BankName = "Bank F", AccountNumber = "6789012345", Balance = 3500 },
        new BankAccount { BankAccountId = 7, EmployeeId = 7, BankName = "Bank G", AccountNumber = "7890123456", Balance = 4000 },
        new BankAccount { BankAccountId = 8, EmployeeId = 8, BankName = "Bank H", AccountNumber = "8901234567", Balance = 4500 },
        new BankAccount { BankAccountId = 9, EmployeeId = 9, BankName = "Bank I", AccountNumber = "9012345678", Balance = 5000 },
        new BankAccount { BankAccountId = 10, EmployeeId = 10, BankName = "Bank J", AccountNumber = "0123456789", Balance = 5500 }
    );
        }

    }
}
