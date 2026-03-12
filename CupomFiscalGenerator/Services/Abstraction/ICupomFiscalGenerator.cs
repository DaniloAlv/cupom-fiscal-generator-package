using CupomFiscalGenerator.Models;

namespace CupomFiscalGenerator.Services.Abstraction
{
    public interface ICupomFiscalGenerator
    {
        void BuildCupomInformation(Company company, Order order, Customer customer);
        byte[] Generate();
    }
}
