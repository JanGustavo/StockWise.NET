# 📦 StockWise.NET

## 🎯 Visão Geral do Projeto

O **StockWise.NET** é um sistema abrangente de gestão empresarial e controle de estoque, desenvolvido para demonstrar proficiência no desenvolvimento **Fullstack** utilizando o ecossistema **C#**. O projeto evoluiu de um MVP (Produto Mínimo Viável) para uma aplicação distribuída, com uma clara separação entre o **Backend (API RESTful)** e o **Frontend (Blazor WebAssembly)**.

Para a persistência de dados, o sistema emprega o **Entity Framework Core** em conjunto com o **PostgreSQL**. A arquitetura do projeto adere a princípios sólidos de engenharia de software, incluindo:

*   **Arquitetura em Camadas (Clean Architecture)**: Promove o desacoplamento e a manutenibilidade.
*   **Injeção de Dependência**: Facilita a testabilidade e a flexibilidade do código.
*   **Documentação OpenAPI**: Garante uma interface de API bem definida e de fácil consumo.
*   **Segurança com BCrypt**: Protege as informações sensíveis dos usuários.

Este repositório serve como um artefato de portfólio prático, destacando a capacidade de construir e escalar uma aplicação desde a concepção até um ambiente **Production-Ready**.

## ✨ Funcionalidades Principais

O StockWise.NET oferece um conjunto robusto de funcionalidades para a gestão eficiente de negócios:

### 📊 Gestão de Estoque e Vendas (CRUD)

Administração completa de produtos (ex: Frutas), permitindo o controle detalhado de:

*   **Entradas e Saídas**: Registro de movimentações de estoque.
*   **Preços**: Definição e atualização de valores de venda.
*   **Quantidades**: Monitoramento do nível de estoque.

### 💰 Controle Financeiro de Pedidos

Registro e gerenciamento de transações, incluindo:

*   **Cálculo de Valor Total**: Automatização do somatório dos itens do pedido.
*   **Classificação de Transações**: Diferenciação entre Vendas (entrada) e Compras/Despesas (saída).

### 👥 Gestão de Clientes e Funcionários

Funcionalidades para gerenciar os principais stakeholders do negócio:

*   **Relacionamentos Complexos**: Estrutura de banco de dados para gerenciar interações entre clientes e funcionários.
*   **Rastreamento de Compras**: Monitoramento do volume de compras por cliente.
*   **Gestão de Cargos e Salários**: Administração da estrutura organizacional e remuneração.

### 🔐 Segurança e Criptografia

Implementação de medidas de segurança robustas:

*   **Hash de Senhas**: Utilização do **BCrypt.Net-Next** para armazenamento seguro de credenciais.

### 🌐 Frontend SPA Responsivo

Uma interface de usuário moderna e interativa:

*   **Blazor WebAssembly**: Desenvolvimento de uma Single Page Application (SPA) responsiva.
*   **Consumo Assíncrono da API**: Interação eficiente com o backend para uma experiência de usuário fluida.

### ⚙️ Configuração Segura

Gerenciamento de configurações sensíveis:

*   **DotNetEnv**: Utilização para carregar variáveis de ambiente de forma segura.
*   **Variáveis Isoladas**: Armazenamento de configurações em arquivos `.env` para isolamento e segurança.

## 🛠️ Tecnologias Utilizadas

O projeto StockWise.NET é construído com as seguintes tecnologias:

### Backend e Dados

*   **C# / .NET 10**: Linguagem e framework principal para o desenvolvimento backend.
*   **Entity Framework Core (ORM)**: Ferramenta para mapeamento objeto-relacional.
*   **Npgsql (PostgreSQL)**: Provedor de dados para interação com o banco de dados PostgreSQL.
*   **BCrypt.Net-Next**: Biblioteca para hashing de senhas.
*   **DotNetEnv**: Gerenciamento de variáveis de ambiente.

### Frontend

*   **Blazor WebAssembly**: Framework para construção de SPAs com C#.
*   **HTML5 / CSS3 / Bootstrap**: Tecnologias padrão para estruturação, estilização e responsividade da interface.

## 🏗️ Arquitetura do Projeto

A solução StockWise.NET é organizada em múltiplos projetos para garantir um alto nível de desacoplamento e modularidade, seguindo os princípios da Clean Architecture.

```
src/
├── StockWise.API/             # Backend (REST API + Swagger)
├── StockWise.Blazor/          # Frontend SPA
├── StockWise.Application/     # Regras de negócio (Services)
├── StockWise.Domain/          # Entidades e interfaces
└── StockWise.Infrastructure/  # Banco de dados e repositórios
```

### 📂 Descrição dos Módulos

*   **StockWise.API**: Atua como o ponto de entrada da aplicação, expondo os endpoints RESTful, configurando o Swagger para documentação e gerenciando a Injeção de Dependência.

*   **StockWise.Blazor**: Contém a interface do usuário (SPA), responsável pela interação com o usuário e consumo da API.

*   **StockWise.Application**: Abriga a lógica de negócio principal e as regras de validação, orquestrando as operações do sistema.

*   **StockWise.Domain**: O núcleo do sistema, definindo as entidades (como Fruta, Pedido, Cliente, Funcionário), interfaces e exceções de domínio.

*   **StockWise.Infrastructure**: Responsável pela integração com o banco de dados, contendo o DbContext e as implementações concretas dos repositórios.

## 🚀 Como Executar o Projeto

Para configurar e executar o projeto StockWise.NET localmente, siga os passos abaixo:

### 📌 Pré-requisitos

Certifique-se de ter os seguintes softwares instalados:

*   **.NET 10 SDK**
*   **PostgreSQL 12+** (pode ser instalado localmente ou via Docker)

### 1. Clonar o Repositório

```bash
git clone https://github.com/JanGustavo/StockWise.NET.git
cd StockWise.NET
```

### 2. Configurar o Banco de Dados

Crie um arquivo `.env` na raiz do projeto a partir do exemplo fornecido:

```bash
cp .env.example .env
```

Edite o arquivo `.env` com suas credenciais e configurações do PostgreSQL:

```
DB_HOST=localhost
DB_PORT=5432
DB_NAME=frutas_db
DB_USER=postgres
DB_PASSWORD=sua_senha_secreta
```

Crie o banco de dados no PostgreSQL (substitua `frutas_db` pelo nome configurado no `.env`):

```bash
psql -U postgres -c "CREATE DATABASE frutas_db;"
```

### 3. Aplicar Migrations

Navegue até a raiz do projeto e execute os comandos para restaurar as dependências e aplicar as migrações do banco de dados:

```bash
dotnet restore
dotnet ef database update \
  --project src/StockWise.Infrastructure \
  --startup-project src/StockWise.API
```

### 4. Rodar a Aplicação

#### Backend (API)

Navegue até o diretório da API e inicie o servidor:

```bash
cd src/StockWise.API
dotnet run
```

A documentação da API estará disponível via Swagger em:

```
http://localhost:<porta>/openapi/v1.json
```

#### Frontend (Blazor)

Navegue até o diretório do Blazor e inicie a aplicação frontend:

```bash
cd src/StockWise.Blazor
dotnet run
```

Abra a URL exibida no terminal em seu navegador para acessar a interface do usuário.

## 🤝 Contribuição

Contribuições são bem-vindas! Para contribuir com o projeto, siga os passos:

1.  Faça um fork do repositório.
2.  Crie uma nova branch para sua funcionalidade ou correção:
    ```bash
    git checkout -b feature/NovaFuncionalidade
    ```
3.  Realize suas alterações e faça commits com mensagens claras:
    ```bash
    git commit -m "feat: Adiciona nova funcionalidade X"
    ```
4.  Envie suas alterações para o seu fork:
    ```bash
    git push origin feature/NovaFuncionalidade
    ```
5.  Abra um Pull Request para o repositório original.

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT**. Consulte o arquivo `LICENSE` na raiz do repositório para mais detalhes.

## 👨‍💻 Autor

Desenvolvido por **Janderson Gustavo**
