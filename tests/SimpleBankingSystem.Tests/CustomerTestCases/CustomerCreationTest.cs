using SimpleBankingSystem.Domain.Entities;

namespace SimpleBankingSystem.Tests.CustomerTests
{
    public class CustomerCreationTest
    {
        [Fact]
        public void Should_ThrowException_When_LastName_Is_Invalid()
        {
            // Arrange
            string lastName = string.Empty;
            string otherNames = "Example";
            DateOnly dob = new (2000, 03, 23);
            string email = "Example@gmail.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Customer(lastName, otherNames, dob, email));
        }

        [Fact]
        public void Should_ThrowException_When_OtherNames_Is_Invalid()
        {
            // Arrange
            string lastName = "Example";
            string otherNames = string.Empty;
            DateOnly dob = new(2000, 03, 23);
            string email = "Example@gmail.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Customer(lastName, otherNames, dob, email));
        }

        [Fact]
        public void Should_ThrowException_When_Customer_Is_Underage()
        {
            // Arrange
            string lastName = "Example";
            string otherNames = "Example";
            DateOnly dob = new(2015, 03, 23);
            string email = "Example@gmail.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Customer(lastName, otherNames, dob, email));
        }

        [Fact]
        public void Should_ThrowException_When_Email_Is_Invalid()
        {
            // Arrange
            string lastName = "Example";
            string otherNames = "Example";
            DateOnly dob = new(2000, 03, 23);
            string email = "Examplegmail.com";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Customer(lastName, otherNames, dob, email));
        }

        [Fact]
        public void Should_Create_Account_When_Data_Is_Valid()
        {
            // Arrange
            string lastName = "Example";
            string otherNames = "Example";
            DateOnly dob = new(2000, 03, 23);
            string email = "Example@gmail.com";

            // Act
            var customer = new Customer(lastName, otherNames, dob, email);

            // Assert
            Assert.Equal("Example", customer.LastName);
            Assert.Equal("Example", customer.OtherNames);
            Assert.Equal(new DateOnly(2000, 03, 23), customer.DateOfBirth);
            Assert.Equal("Example@gmail.com", customer.Email);
        }
    }
}
