using TieredBankAccount.Models;

namespace TieredBankAccount.Data
{
    public class CustomerAddressRepository : IRepository<CustomerAddress>
    {
        private TieredBankAccountContext _context;
        public CustomerAddressRepository(TieredBankAccountContext context)
        {
            _context = context;
        }
        public void Create(CustomerAddress entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CustomerAddress entity)
        {
            throw new NotImplementedException();
        }

        public CustomerAddress? Get(int? id)
        {
            throw new NotImplementedException();
        }

        public ICollection<CustomerAddress> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CustomerAddress entity)
        {
            throw new NotImplementedException();
        }
    }
}
