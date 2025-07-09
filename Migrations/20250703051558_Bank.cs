using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAPI.Migrations
{
    public partial class Bank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.BankAccountId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "BankAccountId", "AccountNumber", "Balance", "BankName", "EmployeeId" },
                values: new object[,]
                {
                    { 1, "1234567890", 1000m, "Bank A", 1 },
                    { 2, "2345678901", 1500m, "Bank B", 2 },
                    { 3, "3456789012", 2000m, "Bank C", 3 },
                    { 4, "4567890123", 2500m, "Bank D", 4 },
                    { 5, "5678901234", 3000m, "Bank E", 5 },
                    { 6, "6789012345", 3500m, "Bank F", 6 },
                    { 7, "7890123456", 4000m, "Bank G", 7 },
                    { 8, "8901234567", 4500m, "Bank H", 8 },
                    { 9, "9012345678", 5000m, "Bank I", 9 },
                    { 10, "0123456789", 5500m, "Bank J", 10 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "password1", "john_doe" },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "password2", "jane_smith" },
                    { 3, "emily.johnson@example.com", "Emily", "Johnson", "password3", "emily_johnson" },
                    { 4, "michael.brown@example.com", "Michael", "Brown", "password4", "michael_williams" },
                    { 5, "sarah.davis@example.com", "Sarah", "Davis", "password5", "sarah_brown" },
                    { 6, "david.miller@example.com", "David", "Miller", "password6", "david_jones" },
                    { 7, "laura.wilson@example.com", "Laura", "Wilson", "password7", "laura_garcia" },
                    { 8, "james.moore@example.com", "James", "Moore", "password8", "james_martinez" },
                    { 9, "linda.taylor@example.com", "Linda", "Taylor", "password9", "linda_hernandez" },
                    { 10, "robert.anderson@example.com", "Robert", "Anderson", "password10", "robert_lopez" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
