# 🚀 Guia de Setup - StockWise.NET

## ✅ Requisitos Atendidos

### Renomagem Completa
- ✅ Namespace: `FrutasDoSeuZe` → `StockWiseNET`
- ✅ Projeto: `.NET 9.0` → `.NET 10.0`
- ✅ 30+ arquivos atualizados
- ✅ Compilação sem erros

### Segurança
- ✅ Arquivo `.env` para credenciais
- ✅ `.env` protegido no `.gitignore`
- ✅ Pacote `DotNetEnv 3.1.1` instalado
- ✅ Documentação de segurança (`SECURITY.md`)

---

## 🔧 Primeiros Passos

### 1. Configurar Variáveis de Ambiente

```bash
cd /home/jandersongustavo/Documentos/Projetos/stockwise/StockWise.NET

# Copiar template
cp .env.example .env

# Editar com suas credenciais
nano .env
```

**Arquivo `.env` deve conter:**
```env
DB_HOST=localhost
DB_PORT=5432
DB_NAME=frutas_db
DB_USER=postgres
DB_PASSWORD=sua_senha_aqui
```

### 2. Verificar se PostgreSQL está rodando

```bash
# Verificar status
sudo systemctl status postgresql

# Ou iniciar se necessário
sudo systemctl start postgresql

# Conectar ao PostgreSQL
psql -U postgres -c "SELECT version();"
```

### 3. Criar banco de dados (se não existir)

```bash
# Conectar como admin
psql -U postgres

# No prompt do psql, executar:
CREATE DATABASE frutas_db;
\q
```

### 4. Executar Migrations

```bash
cd /home/jandersongustavo/Documentos/Projetos/stockwise/StockWise.NET

# Aplicar todas as migrations
dotnet ef database update
```

### 5. Compilar e Executar

```bash
# Compilar
dotnet build StockWise.NET.csproj

# Executar
dotnet run
```

---

## 📋 Estrutura de Variáveis de Ambiente

| Variável | Descrição | Padrão | Obrigatória |
|----------|-----------|---------|-------------|
| `DB_HOST` | Host do servidor PostgreSQL | `localhost` | ❌ |
| `DB_PORT` | Porta do PostgreSQL | `5432` | ❌ |
| `DB_NAME` | Nome do banco de dados | `frutas_db` | ❌ |
| `DB_USER` | Usuário do PostgreSQL | `postgres` | ❌ |
| `DB_PASSWORD` | Senha do PostgreSQL | (vazio) | ✅ |

---

## 🐛 Troubleshooting

### Erro: "password authentication failed"

**Problema:** Credenciais incorretas no `.env`

**Solução:**
```bash
# Verificar credenciais do PostgreSQL
psql -U postgres -h localhost -c "SELECT 1;"

# Atualizar .env com a senha correta
nano .env
```

### Erro: "Could not connect to server"

**Problema:** PostgreSQL não está rodando

**Solução:**
```bash
# Iniciar PostgreSQL
sudo systemctl start postgresql

# Verificar status
sudo systemctl status postgresql
```

### Erro: "Database 'frutas_db' does not exist"

**Problema:** Banco de dados não foi criado

**Solução:**
```bash
# Criar banco de dados
psql -U postgres -c "CREATE DATABASE frutas_db;"

# Executar migrations novamente
dotnet ef database update
```

---

## 🎯 Comandos Úteis

```bash
# Build
dotnet build StockWise.NET.csproj

# Run
dotnet run

# Clean
dotnet clean StockWise.NET.csproj

# Restore
dotnet restore

# Entity Framework - Ver migrations
dotnet ef migrations list

# Entity Framework - Criar nova migration
dotnet ef migrations add NomeDaMigration

# Entity Framework - Aplicar migrations
dotnet ef database update

# Testes (quando adicionado)
dotnet test
```

---

## 📂 Arquivos Importantes

```
├── .env                    # ⚠️ Credenciais (NÃO commitar)
├── .env.example            # 📝 Template para referência
├── SECURITY.md             # 🔒 Documentação de segurança
├── README.md               # 📚 Documentação do projeto
├── Program.cs              # 🎯 Ponto de entrada
├── StockWise.NET.csproj    # ⚙️ Configuração do projeto
└── Data/
    └── AppDbContext.cs     # 🗄️ Context do EF com .env
```

---

## 🔐 Boas Práticas de Segurança

✅ **Sempre fazer:**
- Manter `.env` no `.gitignore`
- Usar `.env.example` como referência
- Atualizar `.env.example` com novas variáveis (sem valores)
- Revisar `.gitignore` antes de fazer commit

❌ **Nunca fazer:**
- Commitar arquivo `.env` com credenciais
- Colocar credenciais em código hardcoded
- Compartilhar `.env` em repositórios públicos
- Commitar senhas ou tokens

---

## 📖 Documentação Completa

- 📚 [README.md](./README.md) - Visão geral do projeto
- 🔒 [SECURITY.md](./SECURITY.md) - Segurança e variáveis de ambiente

---

## ✨ Próximos Passos Recomendados

1. ✅ Configurar `.env`
2. ✅ Criar banco de dados
3. ✅ Executar migrations
4. ✅ Testar a aplicação
5. 🔜 Adicionar testes unitários
6. 🔜 Implementar logs
7. 🔜 Adicionar validações mais robustas

---

**Última atualização:** 13 de abril de 2026
**Status:** ✅ Pronto para uso
**Versão .NET:** 10.0.5
**RootNamespace:** StockWiseNET
