using System.Net.Mail;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Domain.ErrorHandler;

namespace TestSimpleBankingSystem.Tests.EntityBehaviourTest.CustomerEntityTests
{
    public class LinkingAccountToCustomerTest
    {
        [Fact]
        public void Linking_Valid_AccountNumber_To_Customer_Should_Be_Successful()
        {
            // Arrange
            Customer customer = new Customer
                ("Jamie", "Leo Silva", new DateOnly(1998, 05, 18), "jamie@gmail.com");

            // Act
            customer.LinkAccountNumber("1234567890");
            int? count = customer.AccountNumbers.Count;

            // Assert
            Assert.Equal(1, count);
            Assert.Single(customer.AccountNumbers);
        }

        [Fact]
        public void Linking_Invalid_AccountNumber_To_Customer_Should_Throw()
        {
            // Arrange
            Customer customer = new Customer
                ("Jamie", "Leo Silva", new DateOnly(1998, 05, 18), "jamie@gmail.com");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => customer.LinkAccountNumber(string.Empty));
        }
    }
}
