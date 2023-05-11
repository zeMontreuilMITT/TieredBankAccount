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

        public DbSet<TieredBankAccount.Models.Customer> Customer { get; set; } = default!;
        public DbSet<BankAccount> BankAccounts { get; set; } = default!; 
    }
}
