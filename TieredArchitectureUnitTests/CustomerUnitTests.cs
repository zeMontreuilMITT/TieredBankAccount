using Microsoft.EntityFrameworkCore;
using Moq;
using TieredBankAccount.BLL;
using TieredBankAccount.Models;
using TieredBankAccount.Data;
using System.Net.Sockets;

namespace TieredArchitectureUnitTests
{
    [TestClass]
    public class CustomerUnitTests
    {
        public CustomerBusinessLogic customerBusinessLogic { get; set; }
        public IQueryable<Customer> data { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            // create test data
            data = new List<Customer> {
                new Customer{FullName = "Zach", Id = 1},
                new Customer{FullName = "Archie", Id = 2},
                new Customer{FullName = "Tuna", Id = 3},
                new Customer{FullName = "Freya", Id = 4},
                new Customer{FullName = "Oz", Id = 5 } }.AsQueryable();

            // creat a mock of the customer dbset
            Mock<DbSet<Customer>> mockCustomerSet = new Mock<DbSet<Customer>>();

            // provide data to mock DbSet
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // create a mock of the database context
            Mock<TieredBankAccountContext> mockContext = new Mock<TieredBankAccountContext>();

            // setup the mocked context customer property to return an object of the mocked customer set
            // properties that are overridden by setud must be virtual
            mockContext.Setup(c => c.Customer).Returns(mockCustomerSet.Object);


            // mocked objects must have a constructor without parameters
            customerBusinessLogic = new CustomerBusinessLogic(new CustomerRepository(mockContext.Object));
        }

        [TestMethod]
        public void GetCustomer_ReturnsCustomerWithIdOfArgument()
        {
            Customer actualCustomer = data.First();
            Customer queriedCustomer = customerBusinessLogic.GetCustomer(actualCustomer.Id);

            Assert.AreEqual(actualCustomer, queriedCustomer);
        }

        [TestMethod]
        public void GetCustomer_ThrowsInvalidOperationExceptionOnNotFoundId()
        {
            int badId = 10;
            Assert.ThrowsException<InvalidOperationException>(() =>  customerBusinessLogic.GetCustomer(badId));
        }

        [TestMethod]
        public void GetCustomer_ThrowsArgumentNullExceptionOnNoArgument()
        {
            Assert.ThrowsException<ArgumentNullException>(() => customerBusinessLogic.GetCustomer(null));
        }
    }
}