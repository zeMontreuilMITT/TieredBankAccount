namespace TieredBankAccount.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public HashSet<BankAccount> Accounts { get; set; } = new HashSet<BankAccount>();
        public HashSet<CustomerAddress> CustomerAddresses { get; set; } = new HashSet<CustomerAddress>();
    }
}
