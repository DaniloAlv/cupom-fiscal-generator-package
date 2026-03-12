# Pacote Cupom Fiscal Generator

### Biblioteca NÃO OFICIAL, somente de estudos, com o intuito de testes para geração de cupom fiscal com as informações do prestador de serviços e/ou estabelecimento, listando os produtos adquiridos pelo cliente.

Uma biblioteca .NET leve e fácil de usar para gerar cupons fiscais em sua aplicação.

O objetivo desta biblioteca é simplificar a geração de cupons fiscais, oferecendo uma configuração mínima e integração perfeita com o sistema de injeção de dependência do .NET.

## ✨ Funcionalidades

* Integração simples com Dependency Injection do .NET
* Configuração mínima necessária
* Biblioteca leve e fácil de utilizar
* Pensada para aplicações em ambiente de produção

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
  public class PedidoService
  {      
      private readonly ICupomFiscalGenerator _cupomGenerator;

      public PedidoService(ICupomFiscalGenerator cupomGenerator)
      {
          _cupomGenerator = cupomGenerator;
      }

      public void GerarCupom()
      {
          _cupomGenerator.Generate();
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
