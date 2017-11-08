
namespace TechniquesAndPatternsExamples.CustomerExamples
{
    public class CustomerAdapter
    {
        public Customer Adapt(CustomerModel customerModel)
        {
            var address = GetAddress(customerModel);

            return new Customer
            {
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                Address = address
            };
        }

        private static Address GetAddress(CustomerModel customerModel)
        {
            var splittedAddress = customerModel.Address.Split(' ');
            var city = splittedAddress[0].TrimEnd(',');
            var state = splittedAddress[1];
            var postalCode = splittedAddress[2];
            var address = new Address { City = city, State = state, PostalCode = postalCode };
            return address;
        }
    }
}
