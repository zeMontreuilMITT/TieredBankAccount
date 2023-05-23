using TieredBankAccount.Models;

namespace TieredBankAccount.Data
{
    public class BankAccountRepository : IRepository<BankAccount>
    {
        private TieredBankAccountContext _context;
        public BankAccountRepository(TieredBankAccountContext context) {
            _context = context;
        }
        public void Create(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(BankAccount entity)
        {
            _context.DeleteStuff(entity);
            _context.SaveChanges();
        }

        public BankAccount? Get(int? id)
        {
            return _context.BankAccounts.FirstOrDefault(ba => ba.Id == id);
        }

        public ICollection<BankAccount> GetAll()
        {
            return _context.BankAccounts.ToHashSet<BankAccount>();
        }

        public void Update(BankAccount entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
