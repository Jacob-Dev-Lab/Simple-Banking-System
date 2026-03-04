using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Tests.Account
{
    public class AccountDeactivationTest
    {
        [Fact]
        public void Should_Deactivate_An_Activated_Account()
        {
            // Arrange
            Account account = new SavingsAccount(Guid.NewGuid(), "1234567890");

            // Act
            Result result = account.Deactivate();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(account.IsActive);
        }

        [Fact]
        public void Should_Not_Cause_A_Change_On_An_Already_Deactivated_Account()
        {
            // Arrange
            Account account = new SavingsAccount(Guid.NewGuid(), "1234567890");
            account.Deactivate();

            // Act
            Result result = account.Deactivate();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Account is already deactivated.", result.Message);
            Assert.False(account.IsActive);
        }
    }
}
