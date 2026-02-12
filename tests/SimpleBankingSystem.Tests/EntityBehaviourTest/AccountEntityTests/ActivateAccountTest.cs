using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace TestSimpleBankingSystem.Tests.EntityBehaviourTest.AccountEntityTests
{
    public class ActivateAccountTest
    {
        [Fact]
        public void ActivateAccount_Should_Activate_A_Deactivated_Account()
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
        public void Activating_An_Activated_Account_Should_Not_Cause_A_Change()
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
