namespace TieredBankAccount.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
