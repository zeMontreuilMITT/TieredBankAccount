using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public List<BankAccount> data { get; set; }

        public static IEnumerable<object[]> DataArgumentLessThanBalance
        {
            get
            {
                return new[]{
                    new object[]{1, 24.55M, 25.45M},
                    new object[]{2, 5M, 0M}
                };
            }
        }

        public static IEnumerable<object[]> DataArgumentExceedsBalance
        {
            get
            {
                return new[]{
                    new object[]{1, 55M},
                    new object[]{2, 6M}
                };
            }
        }


        [TestInitialize]
        public void Initialize()
        {
            Mock<DbSet<BankAccount>> mockAccountDbSet = new Mock<DbSet<BankAccount>>();

            data = new List<BankAccount>
            {
                new BankAccount{Id = 1, Balance = 50, IsActive = true },
                new BankAccount{Id = 2, Balance = 5, IsActive = true},
                new BankAccount{Id = 3, Balance = 500, IsActive = false }
            };
            
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockAccountDbSet.As<IQueryable<BankAccount>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());
      

            Mock<TieredBankAccountContext> mockContext = new Mock<TieredBankAccountContext>();

            mockContext.Setup(a => a.DeleteStuff(It.IsAny<BankAccount>())).Callback<BankAccount>(a => data.Remove(a));

            mockContext.Setup(c => c.BankAccounts).Returns(mockAccountDbSet.Object);
            

            AccountBusinessLogic = new AccountBusinessLogic(new BankAccountRepository(mockContext.Object));


           
        }

        [TestMethod]
        [DynamicData(nameof(DataArgumentLessThanBalance))]
        public void Withdraw_ArgumentGreaterThanZeroLessThanBalance_SubtractsFromBalance(int accountId, decimal withdrawalAmount, decimal expectedResult)
        {
            BankAccount actualAccount = data.First(a => a.Id == accountId);
            // act
            AccountBusinessLogic.Withdraw(withdrawalAmount, accountId);

            Assert.AreEqual(expectedResult, actualAccount.Balance);
        }

        [TestMethod]
        [DynamicData(nameof(DataArgumentExceedsBalance))]
        public void Withdraw_ArgumentExceedsBalance_ThrowsInvalidOperation(int accountId, decimal withdrawalAmount)
        {
            BankAccount actualAccount = data.First(a => a.Id == accountId);

            Assert.ThrowsException<InvalidOperationException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Withdraw_ArgumentZeroOrLess_ThrowsArgumentRangeException()
        {
            int accountId = 1;
            BankAccount actualAccount = data.First(a => a.Id == accountId);
            decimal withdrawalAmount = -1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Withdraw_AccountNotFound_ThrowsKeyNotFound()
        {
            int accountId = int.MaxValue;
            decimal withdrawalAmount = 1;


            Assert.ThrowsException<KeyNotFoundException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Withdraw_AccountInactive_ThrowsInvalidOperation()
        {
            BankAccount inactiveAccount = data.First(a => !a.IsActive);
            int accountId = inactiveAccount.Id;
            decimal withdrawalAmount = 1;

            Assert.ThrowsException<InvalidOperationException>(() => AccountBusinessLogic.Withdraw(withdrawalAmount, accountId));
        }

        [TestMethod]
        public void Delete_RemovesAccount()
        {
            int initialCount = data.Count();
            BankAccount account = AccountBusinessLogic.GetBankAccount(1);

            AccountBusinessLogic.DeleteAccount(account);

            Assert.AreEqual(initialCount - 1, data.Count);

        }
    }
}
