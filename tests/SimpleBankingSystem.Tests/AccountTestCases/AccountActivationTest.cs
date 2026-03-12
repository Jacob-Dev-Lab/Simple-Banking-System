using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Domain.Entities
{
    public class AccountActivationTest
    {
        [Fact]
        public void Should_Activate_A_Deactivated_Account()
        {
            // Arrange
            Account account = new SavingsAccount(Guid.NewGuid(), "1234567890");
            account.Deactivate();

            // Act
            Result result = account.Activate();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(account.IsActive);
        }

        [Fact]
        public void Should_Not_Cause_A_Change_On_An_Activated_Account()
        {
            // Arrange
            Account account = new SavingsAccount(Guid.NewGuid(), "1234567890");

            // Act
            Result result = account.Activate();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Account is already activated.", result.Message);
            Assert.True(account.IsActive);
        }
    }
}
