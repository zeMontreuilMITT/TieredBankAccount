using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TieredBankAccount.BLL;
using TieredBankAccount.Data;
using TieredBankAccount.Models;

namespace TieredArchitectureUnitTests
{
    [TestClass]
    public class AccountUnitTests
    {
        public AccountBusinessLogic AccountBusinessLogic { get; set; }
        public IQueryable<BankAccount> data { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Mock<DbSet<BankAccount>> mockAccountDbSet = new Mock<DbSet<BankAccount>>();

            data = new List<BankAccount>
            {
                new BankAccount{Id = 1, Balance = 50, IsActive = true },
                new BankAccount{Id = 2, Balance = 5, IsActive = true},
                new BankAccount{Id = 3, Balance = 500, IsActive = false }
            }.AsQueryable();

            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.Provider).Returns(data.Provider);
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.Expression).Returns(data.Expression);
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            Mock<TieredBankAccountContext> mockContext = new Mock<TieredBankAccountContext>();

            mockContext.Setup(c => c.BankAccounts).Returns(mockAccountDbSet.Object);

            AccountBusinessLogic = new AccountBusinessLogic(new BankAccountRepository(mockContext.Object));
        }

        [TestMethod]
        public void Withdraw_SubtractsArgumentFromBalance()
        {
            int accountId = 1;
            decimal withdrawalAmount = 25;
            BankAccount actualAccount = data.First(a => a.Id == accountId);
            decimal expectedResult = actualAccount.Balance - withdrawalAmount;

            // act
            AccountBusinessLogic.Withdraw(withdrawalAmount, accountId);

            Assert.AreEqual(expectedResult, actualAccount.Balance);
        }

        [TestMethod]
        public void Withdraw_ThrowsInvalidOperationIfArgumentExceedsBalance()
        {
            int accountId = 1;
            BankAccount actualAccount = data.First(a => a.Id == accountId);
            decimal withdrawalAmount = actualAccount.Balance + 1;

            Assert.ThrowsException<InvalidOperationException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Withdraw_ThrowsArgumentRangeExceptionIfArgZeroOrLess()
        {
            int accountId = 1;
            BankAccount actualAccount = data.First(a => a.Id == accountId);
            decimal withdrawalAmount = -1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Withdraw_ThrowsKeyNotFoundExceptionIfAccountNotFound()
        {
            int accountId = int.MaxValue;
            decimal withdrawalAmount = 1;


            Assert.ThrowsException<KeyNotFoundException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Withdraw_ThrowsInvalidOperationIfAccountInactive()
        {
            BankAccount inactiveAccount = data.First(a => !a.IsActive);
            int accountId = inactiveAccount.Id;
            decimal withdrawalAmount = 1;

            Assert.ThrowsException<InvalidOperationException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }
    }
}
