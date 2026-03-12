# Pacote Cupom Fiscal Generator

### Biblioteca NÃO OFICIAL, somente de estudos, com o intuito de gerar um cupom fiscal com as informações do prestador de serviços ou estabelecimento, listando os produtos adquiridos pelo cliente.

Uma biblioteca .NET leve e fácil de usar para gerar cupons fiscais em sua aplicação.

O objetivo desta biblioteca é simplificar a geração de cupons fiscais, oferecendo uma configuração mínima e integração fácil.

## ✨ Funcionalidades

* Integração simples com Dependency Injection do .NET
* Configuração mínima necessária
* Biblioteca leve e fácil de utilizar

## 📦 Instalação

Instale o pacote via NuGet:
```bash
dotnet add package CupomFiscalGenerator
```
Ou através do Gerenciador de Pacotes NuGet.

## 🚀 Começando Rápido

Para começar a usar a biblioteca, basta registrar o serviço no container de injeção de dependência da sua aplicação.

### 1. Registrar o serviço

Adicione a seguinte linha no Startup.cs ou Program.cs da sua aplicação:
```c#
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddCupomFiscalGenerator();
  }
}
```

### 2. Utilizar o serviço

Após registrar o serviço, você pode injetá-lo em suas classes.
```c#
[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{      
    private readonly ICupomFiscalGenerator _cupomGenerator;

    public PedidoController(ICupomFiscalGenerator cupomGenerator)
    {
        _cupomGenerator = cupomGenerator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        //{
        // restante do código omitido
        //}

        _cupomGenerator.BuildCupomInformation(empresa, pedido, cliente);
        var cupomFiscal = _cupomGenerator.Generate();

        return File(cupomFiscal, Application.Pdf, "cupom_fiscal.pdf");
    }
}
```

## 📖 Como Funciona

A biblioteca registra internamente todos os serviços necessários quando você executa:
```c#
services.AddCupomFiscalGenerator();
```

> [!NOTE]
> **Informação:** _O serviço foi configurado para funcionar como Scoped_    

Após isso, o gerador de cupom fiscal ficará disponível através de injeção de dependência em qualquer parte da aplicação.

## 📚 Dependências

Esta biblioteca utiliza a seguinte biblioteca para geração de documentos:

- **QuestPDF** – biblioteca moderna para geração de PDFs em .NET

## 🧩 Requisitos:
.NET 8 ou superior

## 🤝 Contribuição

Contribuições são bem-vindas!
Se você quiser melhorar o projeto, fique à vontade para abrir uma issue ou enviar um pull request.

## 📄 Licença

Este projeto está licenciado sob a licença MIT.
