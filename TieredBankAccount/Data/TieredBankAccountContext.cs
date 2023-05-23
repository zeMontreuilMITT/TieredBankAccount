using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TieredBankAccount.Models;

namespace TieredBankAccount.Data
{
    public class TieredBankAccountContext : DbContext
    {
        public TieredBankAccountContext (DbContextOptions<TieredBankAccountContext> options)
            : base(options)
        {
        }

        public TieredBankAccountContext() : base()
        {

        }


        public virtual DbSet<TieredBankAccount.Models.Customer> Customer { get; set; } = default!;
        public virtual DbSet<BankAccount> BankAccounts { get; set; } = default!; 

        public virtual void DeleteCustomer(Customer customer)
        {
            Customer.Remove(customer);
        }
    }
}
