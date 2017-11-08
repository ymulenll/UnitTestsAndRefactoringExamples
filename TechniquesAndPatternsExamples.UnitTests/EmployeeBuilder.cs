using System;

namespace TechniquesAndPatternsExamples.UnitTests
{
    internal class EmployeeBuilder
    {
        private int id = 1;
        private string firstName = "first";
        private string lastName = "last";
        private DateTime birthDate = new DateTime(2000, 1, 1);
        private string street = "street";

        public Employee Build()
        {
            return new Employee(this.id, this.firstName, this.lastName, this.birthDate, this.street);
        }

        public EmployeeBuilder WithFirstName(string newFirstName)
        {
            this.firstName = newFirstName;
            return this;
        }

        public EmployeeBuilder WithLastName(string newLastName)
        {
            this.lastName = newLastName;
            return this;
        }

        public EmployeeBuilder WithBirthDate(DateTime newBirthDate)
        {
            this.birthDate = newBirthDate;
            return this;
        }

        public EmployeeBuilder WithInvalidBirthDate()
        {
            this.birthDate = new DateTime(1000, 1, 1);
            return this;
        }

        public static implicit operator Employee(EmployeeBuilder instance)
        {
            return instance.Build();
        }

    }
}
