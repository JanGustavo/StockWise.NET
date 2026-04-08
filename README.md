# StockWise.NET

## Visão Geral do Projeto

O **StockWise.NET** é uma aplicação de console robusta e eficiente, desenvolvida em C# com .NET 9, projetada para demonstrar um sistema completo de controle de estoque. Este projeto foca na implementação de operações CRUD (Create, Read, Update, Delete) para produtos (representados como 'Frutas' no contexto atual) e na gestão de pedidos, incluindo vendas e reposições. Utilizando o Entity Framework Core para persistência de dados em PostgreSQL, o StockWise.NET exemplifica boas práticas de desenvolvimento de software, como arquitetura em camadas, injeção de dependência e tratamento de exceções customizadas.

Este projeto é ideal para desenvolvedores que buscam um exemplo prático de aplicação .NET com interação com banco de dados relacional, servindo como um excelente portfólio para demonstrar habilidades em desenvolvimento backend.

## Funcionalidades Principais

*   **Gestão Completa de Produtos (CRUD):** Permite cadastrar, listar, atualizar e deletar produtos do estoque.
*   **Controle de Pedidos:** Gerencia o fluxo de pedidos, diferenciando entre vendas e reposições de estoque.
*   **Persistência de Dados:** Armazenamento seguro e eficiente de informações em um banco de dados PostgreSQL.
*   **Interface de Console Interativa:** Menu intuitivo para facilitar a interação do usuário com o sistema.
*   **Tratamento de Exceções:** Implementação de exceções customizadas para uma gestão de erros mais robusta e clara.
*   **Injeção de Dependência:** Utilização de injeção de dependência para promover a modularidade e testabilidade do código.
*   **Integração Contínua (CI):** Configuração de GitHub Actions para automação de builds e testes.

## Tecnologias Utilizadas

O projeto StockWise.NET foi construído com as seguintes tecnologias:

*   **C#:** Linguagem de programação principal.
*   **.NET 9:** Framework de desenvolvimento.
*   **Entity Framework Core (9.0.10):** ORM (Object-Relational Mapper) para interação com o banco de dados.
*   **Npgsql.EntityFrameworkCore.PostgreSQL (9.0.4):** Provedor PostgreSQL para Entity Framework Core.
*   **Microsoft.Extensions.DependencyInjection (9.0.10):** Biblioteca para injeção de dependência.
*   **Microsoft.Extensions.Configuration (9.0.10):** Para gerenciamento de configurações.
*   **Microsoft.Extensions.Configuration.Json (9.0.10):** Para carregar configurações de arquivos JSON.
*   **Microsoft.Extensions.Configuration.UserSecrets (9.0.10):** Para gerenciar segredos de usuário durante o desenvolvimento.
*   **PostgreSQL:** Sistema de gerenciamento de banco de dados relacional.
*   **GitHub Actions:** Para automação de CI/CD.

## Arquitetura do Projeto

O projeto segue uma arquitetura em camadas, promovendo a separação de responsabilidades e facilitando a manutenção e escalabilidade. As principais camadas incluem:

*   `Data/`: Contém o `AppDbContext`, responsável pela configuração do Entity Framework Core e pela interação com o banco de dados.
*   `Models/`: Define as classes de modelo (`Fruta`, `ItemPedido`, `Pedido`) que representam as entidades do domínio da aplicação.
*   `Repositories/`: Contém as interfaces e implementações dos repositórios, abstraindo a lógica de acesso a dados.
*   `Services/`: Abriga a lógica de negócio da aplicação, orquestrando as operações e utilizando os repositórios.
*   `Exceptions/`: Define exceções customizadas para tratar cenários específicos da aplicação de forma controlada.
*   `Program.cs`: O ponto de entrada da aplicação, responsável pela configuração da injeção de dependência, inicialização do banco de dados e apresentação do menu interativo ao usuário.

## Como Executar o Projeto

Para configurar e executar o StockWise.NET em sua máquina local, siga os passos abaixo:

### Pré-requisitos

*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   [PostgreSQL](https://www.postgresql.org/download/)
*   Um editor de código como [Visual Studio](https://visualstudio.microsoft.com/vs/) ou [VS Code](https://code.visualstudio.com/)

### Configuração

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/JanGustavo/StockWise.NET.git
    cd StockWise.NET
    ```

2.  **Configure o Banco de Dados:**
    *   Crie um banco de dados PostgreSQL. Você pode usar um cliente como `pgAdmin` ou a linha de comando `psql`.
    *   Atualize a string de conexão no arquivo `appsettings.json` (ou `appsettings.Development.json` para desenvolvimento) com as credenciais do seu banco de dados PostgreSQL. Exemplo:
        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "Host=localhost;Port=5432;Database=StockWiseDb;Username=your_username;Password=your_password"
          }
        }
        ```

3.  **Aplique as Migrações do Entity Framework Core:**
    Navegue até o diretório do projeto (`StockWise.NET`) no terminal e execute os seguintes comandos para criar o esquema do banco de dados:
    ```bash
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```

    A aplicação de console será iniciada, apresentando um menu interativo para você começar a gerenciar o estoque.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir *issues* para relatar bugs ou sugerir melhorias, e enviar *pull requests* com novas funcionalidades ou correções.

## Licença

Este projeto está licenciado sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

---

**Desenvolvido por:** Janderson Gustavo
