using CupomFiscalGenerator.Models;
using CupomFiscalGenerator.Services.Abstraction;
using CupomFiscalGenerator.TestConsole;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();

var company = Company.CreateCompany("Maitech Store", "40.923.795/0001-24", "338961818483", "41977437-8",
    "Rua Onze de Junho, 317, Afogados, Recife - PE, 50770-780");

var customer = Customer.CreateCustomer("Lara Isabela Antonella Santos", "776.376.649-29",
    "Rua Raimundo de Medeiros Dantas, 528, Neópolis, Natal - RN, 59080-450");

var orderItens = new List<OrderItem>
{
    OrderItem.CreateItem(07, "Teclado Mecânico Kumara Red", 2, 147.9m),
    OrderItem.CreateItem(23, "Carregador USB-C iPhone", 1, 79.9m),
    OrderItem.CreateItem(11, "Mouse Logitech", 2, 54.9m),
};

var order = Order.CreateOrder(2671, "Cartão de Crédito", orderItens);

using var serviceProvider = serviceCollection.BuildServiceProvider();

var cupomFiscalGenerator = serviceProvider.GetRequiredService<ICupomFiscalGenerator>();

cupomFiscalGenerator.BuildCupomInformation(company, order, customer);
byte[] cupomFiscal = cupomFiscalGenerator.Generate();

using var memory = new MemoryStream(cupomFiscal);
using var pdfFile = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "cupom_fiscal_teste.pdf"), FileMode.Create);
memory.CopyTo(pdfFile);
