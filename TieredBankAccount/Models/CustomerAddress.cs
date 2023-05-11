namespace TieredBankAccount.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
    }
}
