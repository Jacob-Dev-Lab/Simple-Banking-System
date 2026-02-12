using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace TestSimpleBankingSystem.Tests.EntityBehaviourTest.AccountEntityTests
{
    public class DepositBehaviouralTest
    {
        [Fact]
        public void Valid_Amount_Deposit_Should_Increase_Balance()
        {
            // Arrange
            Account account = new SavingsAccount(Guid.NewGuid(), "1234567890");

            // Act
            Result result = account.Deposit(100);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Invalid_Amount_Deposit_Should_Not_Affect_Balance()
        {
            // Arrange
            Account account = new CurrentAccount(Guid.NewGuid(), "2345678901");
            account.Deposit(100);

            // Act
            Result result = account.Deposit(-30);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Amount must be greater than Zero.", result.Message);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Deposit_To_A_Deactivated_Account_Should_Be_Impossible()
        {
            // Arrange
            Account account = new CurrentAccount(Guid.NewGuid(), "2345678901");
            account.Deactivate();

            // Act
            Result result = account.Deposit(100);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Transaction canceled: account is deactivated.", result.Message);
            Assert.Equal(0, account.Balance);
        }
    }
}
