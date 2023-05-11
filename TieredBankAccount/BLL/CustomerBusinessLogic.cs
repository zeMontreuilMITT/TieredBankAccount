using TieredBankAccount.Data;
using TieredBankAccount.Models;

namespace TieredBankAccount.BLL
{
    public class CustomerBusinessLogic
    {
        private IRepository<Customer> _customerRepo;
        private IRepository<BankAccount> _accountRepo;
        public CustomerBusinessLogic(IRepository<Customer> customerRepo, IRepository<BankAccount> accountRepo) {
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
        }

    }
}
