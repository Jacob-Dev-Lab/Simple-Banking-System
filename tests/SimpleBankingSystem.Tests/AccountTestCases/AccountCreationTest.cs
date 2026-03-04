using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Tests.EntityBehaviourTest.AccountEntityTests
{
    public class AccountCreationTest
    {
        [Fact]
        public void Should_ThrowException_When_CustomerID_Is_Invalid()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new SavingsAccount(Guid.Empty, "1234567890"));
        }

        [Fact]
        public void Should_ThrowException_When_AccountNumber_Is_Invalid()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new SavingsAccount(Guid.NewGuid(), "123"));
        }

        [Fact]
        public void Should_Create_Account_When_Data_Is_Valid()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var accountNumber = "0123456789";

            // Act
            var account = new CurrentAccount(customerId, accountNumber);

            // Assert
            Assert.True(account.IsActive);
            Assert.Equal("1234567890", account.AccountNumber);
            Assert.Equal(0, account.Balance);
            Assert.Equal(0, account.Id);
        }
    }
}
