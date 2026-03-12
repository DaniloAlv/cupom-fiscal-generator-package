namespace CupomFiscalGenerator.Models
{
    /// <summary>
    /// Empresa prestadora dos serviços, ou estabelecimento, presente nop cupom fiscal, que efetuou a venda.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Nome fantasia da empresa.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Documento CNPJ da empresa. Informá-lo sem as pontuações.
        /// </summary>
        public string Cnpj { get; private set; }

        /// <summary>
        /// Inscrição Municipal da empresa.
        /// </summary>
        public string IM { get; private set; }

        /// <summary>
        /// Inscrição Estadual da empresa.
        /// </summary>
        public string IE { get; private set; }

        /// <summary>
        /// Localização do endereço de operação da empresa.
        /// </summary>
        public string Address { get; private set; }

        private Company(string name, string cnpj, string im, string ie, string address)
        {
            Name = name;
            Cnpj = cnpj;
            IM = im;
            IE = ie;
            Address = address;
        }

        /// <summary>
        /// Cria de maneira estática um objeto do tipo Company.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cnpj"></param>
        /// <param name="im"></param>
        /// <param name="ie"></param>
        /// <param name="address"></param>
        /// <returns>Objeto do tipo Company</returns>
        public static Company CreateCompany(string name, string cnpj, string im, string ie, string address) =>
            new Company(name, cnpj, im, ie, address);
    }
}
