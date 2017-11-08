
using System;
using NUnit.Framework;

namespace TechniquesAndPatternsExamples.UnitTests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void GetFullName_WhenCalled_ReturnsNameCombination()
        {
            // Eliminates the irrelevant, and amplifies the essentials of the test.
            var employee = new EmployeeBuilder()
                .WithFirstName("firstName")
                .WithLastName("lastName")
                .Build();

            var fullName = employee.GetFullName();

            Assert.AreEqual("firstName lastName", fullName);
        }

        [Test]
        public void Validate_InvalidBirthDate_ReturnsCorrectMessage()
        {
            Employee employee = new EmployeeBuilder()
                .WithBirthDate(new DateTime(1000, 1, 1));

            var message = employee.Validate();

            StringAssert.Contains("Invalid birth date", message);
        }

        [Test]
        public void Validate_ValidBirthDate_ReturnsEmptyMessage()
        {
            var employee = new EmployeeBuilder().Build();

            var message = employee.Validate();

            Assert.IsEmpty(message);
        }

        private Employee CreateEmployee()
        {
            return new Employee(1, "firstName", "lastName", new DateTime(2000, 1, 1), "street");
        }

        private Employee CreateEmployee(string firstName, string lastName)
        {
            return new Employee(1, firstName, lastName, new DateTime(2000, 1, 1), "street");
        }

        private Employee CreateEmployee(DateTime birthDate)
        {
            return new Employee(1, "firstName", "lastName", birthDate, "street");
        }
    }
}
