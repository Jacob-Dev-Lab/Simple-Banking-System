using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace SimpleBankingSystem.Tests.CustomerTests
{
    public class UpdateCustomerProfileTest
    {
        [Fact]
        public void UpdateEmail_Should_Change_CurrentEmail_To_New_ValidEmail()
        {
            // Arrange
            var customer = new Customer
                ("Jamie", "Leo Silva", new DateOnly(1998, 05, 18), "jamie@gmail.com");

            var email = "james@gmail.com";

            // Act
            Result result = customer.ChangeEmailAddress(email);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(email, customer.Email);
        }

        [Fact]
        public void ChangeEmail_With_An_InvalidEmail_Should_Throw()
        {
            // Arrange
            var customer = new Customer
                ("Jamie", "Leo Silva", new DateOnly(1998, 05, 18), "jamie@gmail.com");

            // Act & Assert
            Assert.Throws<FormatException>(() => customer.ChangeEmailAddress("james.com"));
        }

        [Fact]
        public void ChangeLastName_Should_Change_CurrentLastName_To_New_ValidLastName()
        {
            // Arrange
            var customer = new Customer
                ("Jamie", "Leo Silva", new DateOnly(1998, 05, 18), "jamie@gmail.com");

            // Act
            Result result = customer.ChangeLastname("Malcom");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Malcom", customer.LastName);
        }

        [Fact]
        public void ChangeLastName_With_An_InvalidLastName_Should_Return_ErrorMessage()
        {
            // Arrange
            var customer = new Customer
                ("Jamie", "Leo Silva", new DateOnly(1998, 05, 18), "jamie@gmail.com");

            // Act
            Result result = customer.ChangeLastname(string.Empty);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("A valid Last name is required.", result.Message);
            Assert.Equal("Jamie", customer.LastName);
        }
    }
}
