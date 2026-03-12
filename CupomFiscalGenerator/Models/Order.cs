using System.Collections;
using System.ComponentModel;

namespace CupomFiscalGenerator.Models
{
    /// <summary>
    /// Pedido contendo as informações dos itens adicionados pelo cliente na compra.
    /// </summary>
    public class Order : IEnumerable<OrderItem>
    {
        private IList<OrderItem> _orderItems;

        public int Id { get; private set; }
        public decimal Total => _orderItems.Sum(oi => oi.Value * oi.Amount);
        public string PaymentMethod { get; private set; }

        private Order(int id, string paymentMethod, IList<OrderItem> items)
        {
            _orderItems = items;

            Id = id;
            PaymentMethod = paymentMethod;
        }

        public IEnumerator<OrderItem> GetEnumerator() => _orderItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Método estático que cria uma instância de Order.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="total"></param>
        /// <param name="paymentMethod"></param>
        /// <returns>Objeto do tipo Order</returns>
        public static Order CreateOrder(int id, string paymentMethod, IList<OrderItem> items) => new Order(id, paymentMethod, items);
    }

    /// <summary>
    /// Item que faz parte de um pedido.
    /// </summary>
    public class OrderItem 
    {
        public int Id { get; private set; }

        [DisplayName("Nome")]
        public string Name { get; private set; }

        [DisplayName("Quantidade")]
        public int Amount { get; private set; }

        [DisplayName("Valor Unitário")]
        public decimal Value { get; private set; }

        private OrderItem(int id, string name, int amount, decimal value)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Value = value;
        }

        /// <summary>
        /// Método para criação de um objeto OrderItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="value"></param>
        /// <returns>Objeto do tipo OrderItem</returns>
        public static OrderItem CreateItem(int id, string name, int amount, decimal value)
        {
            return new OrderItem(id, name, amount, value);
        }
    }
}
