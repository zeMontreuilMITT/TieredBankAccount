using TieredBankAccount.Models;

namespace TieredBankAccount.Data
{
    public class CustomerRepository : IRepository<Customer>
    {
        private TieredBankAccountContext _context;

        public CustomerRepository(TieredBankAccountContext context)
        {
            _context = context;
        }

        public void Create(Customer entity)
        {
            _context.Customer.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Customer entity)
        {
            _context.DeleteCustomer(entity);
        }

        public ICollection<Customer> GetAll()
        {
            return _context.Customer.ToHashSet<Customer>();
        }

        public Customer? Get(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException();
            }

            return _context.Customer.First(c => c.Id == id);
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
