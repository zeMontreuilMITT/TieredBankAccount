using TieredBankAccount.Data;
using TieredBankAccount.Models;

namespace TieredBankAccount.BLL
{
    public class AccountBusinessLogic
    {
        private IRepository<BankAccount> _accountRepository;
        public AccountBusinessLogic(IRepository<BankAccount> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public BankAccount GetBankAccount(int? accountId)
        {
            if (accountId == null)
            {
                throw new NullReferenceException("AccountId cannot be null");
            }
            else
            {
                BankAccount account = _accountRepository.Get(accountId);

                if (account != null)
                {
                    return account;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }


        public void Withdraw(decimal amount, int accountId)
        {
            BankAccount account = GetBankAccount(accountId);

            if (account.IsActive)
            {
                if(amount > account.Balance)
                {
                    throw new Exception("Withdrawal amount cannot exceed account balance.");
                } else
                {
                    account.Balance -= amount;
                    _accountRepository.Update(account);
                }
            }
            else
            {
                throw new Exception("Selected account is not active.");
            }
        }
    }
}
