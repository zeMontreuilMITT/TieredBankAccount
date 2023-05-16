namespace TieredBankAccount.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public HashSet<CustomerAddress> CustomerAddresses { get; set; } = new HashSet<CustomerAddress>();

        public virtual void SayHello()
        {
            Console.WriteLine($"Hello, I live at {AddressLine}");
        }

    }
}
