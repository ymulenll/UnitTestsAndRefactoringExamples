using FluentAssertions;
using NUnit.Framework;
using TechniquesAndPatternsExamples.CustomerExamples;

namespace TechniquesAndPatternsExamples.UnitTests.CustomerExamples
{
    [TestFixture]
    public class CustomerAdapterTests
    {
        [Test]
        public void Adapt_CorrectInput_ReturnsCorrectCustomer()
        {
            var customerModel = new CustomerModel
            {
                FirstName = "Bill",
                LastName = "Sans",
                Address = "Portland, OR 12345"
            };

            var expectedCustomer = new Customer
            {
                FirstName = "Bill",
                LastName = "Sans",
                Address = new Address
                {
                    City = "Portland",
                    State = "OR",
                    PostalCode = "12345"
                }
            };
            //.ToExpectedObject(context => context.Ignore(customer => customer.LastName));

            var customerAdapter = this.CreateCustomerAdapter();

            var actualCustomer = customerAdapter.Adapt(customerModel);

            //expectedCustomer.ShouldEqual(actualCustomer);

            expectedCustomer.ShouldBeEquivalentTo(actualCustomer);
        }

        private CustomerAdapter CreateCustomerAdapter()
        {
            return new CustomerAdapter();
        }
    }
}
