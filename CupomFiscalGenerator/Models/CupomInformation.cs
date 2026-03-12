namespace CupomFiscalGenerator.Models
{
    /// <summary>
    /// Carrega as informações necessárias para o conteúdo presente em um cupom fiscal.
    /// </summary>
    public class CupomInformation
    {
        /// <summary>
        /// ID da transação gerada para o cupom fiscal.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Informações da empresa prestadora de serviços ou estabelecimento dono dos produtos vendidos.
        /// </summary>
        public Company Company { get { return _company; } private set { _company = value; } }

        /// <summary>
        /// Informações do cliente que efetuou um pedido no estabelecimento ou prestador de serviços.
        /// </summary>
        public Customer Customer { get { return _customer; } private set { _customer = value; } }

        /// <summary>
        /// Informações do pedido, com a listagem de todos os itens adquiridos na compra.
        /// </summary>
        public Order Order { get { return _order; } private set { _order = value; } }


        private static Company _company;
        private static Customer _customer;
        private static Order _order;

        private CupomInformation(Company company, Customer customer, Order order)
        {
            TransactionId = Guid.NewGuid();
            _company = company;
            _customer = customer;
            _order = order;
        }

        public static CupomInformation CreateCupomInformation(Company company, Customer customer, Order order) => 
            new CupomInformation(company, customer, order);
    }
}
