using TieredBankAccount.Models;

namespace TieredBankAccount.Data
{
    public class AddressRepository : IRepository<Address>
    {
        private TieredBankAccountContext _context;
        public AddressRepository(TieredBankAccountContext context)
        {
            _context = context;
        }

        public void Create(Address entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Address entity)
        {
            throw new NotImplementedException();
        }

        public Address? Get(int? id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Address> GetAll()
        {

            throw new NotImplementedException();
        }

        public void Update(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
