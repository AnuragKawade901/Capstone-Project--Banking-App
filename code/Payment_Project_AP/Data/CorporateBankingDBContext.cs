using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Payment_Project_AP.Data
{
    public class CorporateBankingDBContext : DbContext
    {
        public CorporateBankingDBContext(DbContextOptions options) : base(options) { }
        //public BankingPaymentsDBContext() { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankUser> BankUsers { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<SalaryDisbursement> SalaryDisbursements { get; set; }
        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<Support> Supports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountStatus>()
                .HasData(
                    new AccountStatus { StatusId = 1, Status = AccStatus.ACTIVE },
                    new AccountStatus { StatusId = 2, Status = AccStatus.INACTIVE },
                    new AccountStatus { StatusId = 3, Status = AccStatus.CLOSED }
                );

            modelBuilder.Entity<AccountType>()
                .HasData(
                    new AccountType { TypeId = 1, Type = AccType.SAVINGS },
                    new AccountType { TypeId = 2, Type = AccType.CURRENT },
                    new AccountType { TypeId = 3, Type = AccType.SALARY }
                );

            modelBuilder.Entity<PaymentStatus>()
                .HasData(
                    new PaymentStatus { StatusId = 1, Status = PayStatus.APPROVED },
                    new PaymentStatus { StatusId = 2, Status = PayStatus.DECLINED },
                    new PaymentStatus { StatusId = 3, Status = PayStatus.PENDING }
                );

            modelBuilder.Entity<DocumentType>()
                .HasData(
                    new DocumentType { TypeId = 1, Type = DocType.IDENTITY_PROOF },
                    new DocumentType { TypeId = 2, Type = DocType.ADDRESS_PROOF },
                    new DocumentType { TypeId = 3, Type = DocType.DATE_OF_BIRTH_PROOF },
                    new DocumentType { TypeId = 4, Type = DocType.PHOTOGRAPH },
                    new DocumentType { TypeId = 5, Type = DocType.PAN_CARD },
                    new DocumentType { TypeId = 6, Type = DocType.OTHER }
                );

            modelBuilder.Entity<TransactionType>()
                .HasData(
                    new TransactionType { TypeId = 1, Type = TransType.CREDIT },
                    new TransactionType { TypeId = 2, Type = TransType.DEBIT }
                );

            modelBuilder.Entity<UserRole>()
                .HasData(
                    new UserRole { RoleId = 1, Role = Role.ADMIN },
                    new UserRole { RoleId = 2, Role = Role.BANK_USER },
                    new UserRole { RoleId = 3, Role = Role.CLIENT_USER }
                );

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PayerAccount)
                .WithMany()
                .HasForeignKey(p => p.PayerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryDisbursement>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SalaryPayment>()
                .HasOne(d => d.SalaryDisbursement)
                .WithMany(s => s.DisbursementDetails)
                .HasForeignKey(d => d.SalaryDisbursementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryPayment>()
                .HasOne(d => d.Employee)
                .WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryPayment>()
                .HasOne(d => d.Transaction)
                .WithOne()
                .HasForeignKey<SalaryPayment>(d => d.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryPayment>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.SalaryPayment)
                .HasForeignKey(s => s.EmployeeId);

            modelBuilder.Entity<SalaryPayment>()
                .HasOne(s => s.SalaryDisbursement)
                .WithMany(d => d.DisbursementDetails)
                .HasForeignKey(s => s.SalaryDisbursementId);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.BankUser)
                .WithMany(b => b.Clients)
                .HasForeignKey(c => c.BankUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
