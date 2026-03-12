namespace CupomFiscalGenerator.Models
{
    public class Customer
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Address { get; private set; }

        private Customer(string name, string cpf, string address)
        {
            Name = name;
            Cpf = cpf;
            Address = address;
        }

        public static Customer CreateCustomer(string name, string cpf, string address)
        {
            return new Customer(name, cpf, address);
        }
    }
}
