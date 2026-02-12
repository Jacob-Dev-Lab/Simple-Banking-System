using SimpleBankingSystem.Domain;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace TestSimpleBankingSystem.Tests.EntityBehaviourTest.AccountEntityTests
{
    public class DeactivateAccountTest
    {
        [Fact]
        public void DeactivateAccount_Should_Deactivate_An_Activated_Account()
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
        public void Deactivating_A_Deactivated_Account_Should_Not_Cause_A_Change()
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
