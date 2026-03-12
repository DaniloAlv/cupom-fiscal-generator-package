using CupomFiscalGenerator.Models;
using CupomFiscalGenerator.Services.Abstraction;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace CupomFiscalGenerator.Services
{
    internal class GeneratorService : ICupomFiscalGenerator
    {
        private CupomInformation _cupomInformation;

        public void BuildCupomInformation(Company company, Order order, Customer customer) =>
            _cupomInformation = CupomInformation.CreateCupomInformation(company, customer, order);

        /// <summary>
        /// Responsável por gerar o cupom fiscal com os dados fornecidos da compra.
        /// </summary>
        /// <returns>Um byte[] referente ao arquivo PDF gerado</returns>
        public byte[] Generate()
        {
            CupomInformation information = _cupomInformation;

            Settings.License = LicenseType.Community;

            var document = Document.Create(doc =>
            {
                doc.Page(pg =>
                {
                    pg.Margin(1.5f, Unit.Centimetre);
                    pg.Size(PageSizes.A4);
                    pg.PageColor(Colors.Grey.Lighten1);
                    pg.DefaultTextStyle(text =>
                        text.FontSize(12)
                            .FontColor(Colors.Black)
                            .FontFamily("Times New Roman", "Courier New")
                    );

                    pg.Header()
                        .Element(container =>
                        {
                            container.Row(row =>
                            {
                                row.RelativeItem()
                                    .Column(col =>
                                    {
                                        col.Item()
                                            .Text(text =>
                                            {
                                                text.Line(information.Company.Name)
                                                    .FontSize(18)
                                                    .Black()
                                                    .FontColor(Colors.Grey.Darken3);

                                                text.Line(information.Company.Address).FontSize(12);
                                            });                                      
                                    });

                                row.RelativeItem()
                                    .AlignRight()
                                    .Column(col =>
                                    {
                                        col.Item()
                                            .Text(text =>
                                            {
                                                text.Span("CNPJ: ")
                                                    .SemiBold()
                                                    .FontSize(11);
                                                text.Span(information.Company.Cnpj)
                                                    .Medium()
                                                    .FontSize(11);
                                            });

                                        col.Item()
                                            .Text(text =>
                                            {
                                                text.Span("IE: ")
                                                    .SemiBold()
                                                    .FontSize(11);
                                                text.Span(information.Company.IE)
                                                    .FontSize(11);
                                            });
                                        col.Item()
                                            .Text(text =>
                                            {
                                                text.Span("IM: ")
                                                    .SemiBold()
                                                    .FontSize(11);
                                                text.Span(information.Company.IM)
                                                    .FontSize(11);
                                            });
                                    });
                            });
                        });

                    pg.Content()
                        .Element(container =>
                        {
                            container.Row(row =>
                            {
                                row.RelativeItem()
                                    .Column(col =>
                                    {
                                        col.Item()
                                            .Padding(30)
                                            .AlignCenter()
                                            .Text(text =>
                                            {
                                                text.Span("PEDIDO Nº ").Black();
                                                text.Span(information.Order.Id.ToString()).Medium();
                                            });

                                        col.Item()
                                            .AlignRight()
                                            .PaddingBottom(10)
                                            .Text(text => text.Span(DateTime.UtcNow.ToString("dd/MM/yyyy")));

                                        col.Item()
                                            .Table(table =>
                                            {
                                                table.ColumnsDefinition(columns =>
                                                {
                                                    columns.ConstantColumn(50);
                                                    columns.ConstantColumn(50);
                                                    columns.RelativeColumn();
                                                    columns.ConstantColumn(80);
                                                    columns.ConstantColumn(100);
                                                });

                                                table.Header(header =>
                                                {
                                                    header.Cell().BorderBottom(1).Padding(8).Text("#").Black();
                                                    header.Cell().BorderBottom(1).Padding(8).Text("ID").Black();
                                                    header.Cell().BorderBottom(1).Padding(8).Text("Nome").Black();
                                                    header.Cell().BorderBottom(1).Padding(8).Text("Quantidade").Black();
                                                    header.Cell().BorderBottom(1).Padding(8).AlignRight().Text("Valor Unitário").Black();
                                                });

                                                uint orderItem = 1;
                                                foreach (var item in information.Order)
                                                {
                                                    table.Cell().BorderRight(1).Padding(8).Text(orderItem.ToString());
                                                    table.Cell().BorderRight(1).Padding(8).Text(item.Id.ToString());
                                                    table.Cell().BorderRight(1).Padding(8).Text(item.Name);
                                                    table.Cell().BorderRight(1).Padding(8).Text(item.Amount.ToString());
                                                    table.Cell().Padding(8).AlignRight().Text(item.Value.ToString("C2", new CultureInfo("pt-BR")));

                                                    orderItem++;
                                                }
                                            });

                                        col.Item()
                                            .PaddingTop(40)
                                            .Table(table =>
                                            {
                                                table.ColumnsDefinition(def =>
                                                {
                                                    def.ConstantColumn(200);
                                                    def.ConstantColumn(300);
                                                });
                                                table.Header(cell =>
                                                {
                                                    cell.Cell().Padding(4).AlignLeft().Text("PAGAMENTO").SemiBold();
                                                    cell.Cell().Padding(4).AlignRight().Text("TOTAL").SemiBold();
                                                });
                                                table.Cell().Padding(4).AlignLeft().Text(information.Order.PaymentMethod);
                                                table.Cell().Padding(4).AlignRight().Text(information.Order.Total.ToString("C2", new CultureInfo("pt-BR")));
                                            });

                                        col.Item()
                                            .PaddingTop(50)
                                            .AlignCenter()
                                            .Text(text => text.Line("DADOS DO CLIENTE").Black());

                                        col.Item()
                                            .Table(table =>
                                            {
                                                table.ColumnsDefinition(def =>
                                                {
                                                    def.RelativeColumn();
                                                    def.ConstantColumn(300);
                                                });
                                                table.Cell().Padding(4).AlignLeft().Text(information.Customer.Name);
                                                table.Cell().Padding(4).AlignRight().Text(text =>
                                                {
                                                    text.Span("CPF: ").SemiBold();
                                                    text.Span(information.Customer.Cpf);
                                                });
                                            });
                                        col.Item()
                                            .AlignLeft()
                                            .Padding(4)
                                            .Text(information.Customer.Address);
                                    });
                            });
                        });

                    pg.Footer()
                        .Element(container =>
                        {
                            container.Row(row =>
                            {
                                row.RelativeItem()
                                    .Column(col =>
                                    {
                                        col.Item()
                                            .Text(text =>
                                            {
                                                text.Line("© Copyright - SoftwareCupomFiscalGenerator");
                                            });
                                    });

                                row.RelativeItem()
                                    .AlignRight()
                                    .Column(col =>
                                    {
                                        col.Item()
                                            .Text(text =>
                                            {
                                                text.Span(information.TransactionId.ToString());
                                            });
                                    });                                
                            });
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
