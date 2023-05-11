namespace TieredBankAccount.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public HashSet<CustomerAddress> CustomerAddresses { get; set; } = new HashSet<CustomerAddress>();

    }
}
