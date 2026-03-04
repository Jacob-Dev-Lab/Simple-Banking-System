using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Tests.Account
{
    public class DepositBehaviouralTest
    {
        [Fact]
        public void Should_Increase_Balance_When_Amount_Is_Valid()
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
        public void Should_Not_Affect_Balance_When_Amount_Is_Invalid()
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
        public void Should_Be_Impossible_On_A_Deactivated_Account()
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
