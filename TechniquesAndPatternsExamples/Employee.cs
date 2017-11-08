using System;

namespace TechniquesAndPatternsExamples
{
    public class Employee
    {
        public Employee(int id, string firtName, string lastName, DateTime birthDate, string street)
        {
            this.Id = id;
            this.FirtName = firtName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Street = street;
        }

        public int Id { get; private set; }

        public string FirtName { get; private set; }

        public string LastName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Street { get; private set; }

        public string GetFullName()
        {
            return this.FirtName + " " + this.LastName;
        }

        public string Validate()
        {
            if (this.BirthDate < new DateTime(1800, 1, 1))
            {
                return "Invalid birth date.";
            }

            return "";
        }
    }
}
