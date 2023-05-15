using TieredBankAccount.Data;
using TieredBankAccount.Models;

namespace TieredBankAccount.BLL
{
    public class CustomerBusinessLogic
    {
        private IRepository<Customer> _customerRepo;

        public CustomerBusinessLogic(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public Customer GetCustomer(int? customerId)
        {
            return _customerRepo.Get(customerId);
        }

        public ICollection<BankAccount> GetAllCustomerAccounts(int? customerId)
        {
            return new List<BankAccount>();
        }


    }
}
