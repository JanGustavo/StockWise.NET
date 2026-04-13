# 🔒 Segurança e Variáveis de Ambiente

## Proteção de Dados Sensíveis

O projeto **StockWise.NET** implementa boas práticas de segurança para proteger informações sensíveis como credenciais de banco de dados.

## Como Funciona

### Arquivo `.env`

As credenciais sensíveis são armazenadas em um arquivo `.env` que:
- ✅ É carregado automaticamente ao iniciar a aplicação via `DotNetEnv` 
- ❌ **NÃO** é versionado no Git (está no `.gitignore`)
- ❌ **NÃO** é commitado no repositório
- 📝 Possui um arquivo `.env.example` como referência

### Configuração Atual

```csharp
// AppDbContext.cs
DotNetEnv.Env.Load(); // Carrega variáveis do .env

var host = Environment.GetEnvironmentVariable("DB_HOST");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
// ... etc
```

## Setup para Desenvolvimento

### 1. Copiar arquivo de exemplo

```bash
cp .env.example .env
```

### 2. Editar `.env` com suas credenciais

```env
DB_HOST=localhost
DB_PORT=5432
DB_NAME=frutas_db
DB_USER=postgres
DB_PASSWORD=sua_senha_do_postgres
```

### 3. Nunca commitar o `.env`

O arquivo está protegido no `.gitignore`:

```gitignore
## Environment variables
.env
.env.local
.env.*.local
```

## Alternativas para Produção

Para ambientes de produção, considere:

### 1. **Variáveis de Ambiente do Sistema Operacional**
```bash
export DB_PASSWORD="senha_segura"
dotnet run
```

### 2. **Azure Key Vault** (para Azure)
```csharp
var keyVaultUrl = new Uri("https://myvault.vault.azure.net/");
var credential = new DefaultAzureCredential();
var client = new SecretClient(keyVaultUrl, credential);
var secret = client.GetSecret("db-password");
```

### 3. **Docker Secrets** (para Docker)
```yaml
services:
  app:
    environment:
      DB_PASSWORD_FILE: /run/secrets/db_password
```

### 4. **Kubernetes Secrets** (para Kubernetes)
```yaml
apiVersion: v1
kind: Secret
metadata:
  name: db-credentials
type: Opaque
data:
  password: c2VuaGE=
```

## Verificação de Segurança

✅ **Implementado:**
- [ ] Variáveis de ambiente carregadas do `.env`
- [ ] Arquivo `.env` no `.gitignore`
- [ ] Arquivo `.env.example` como referência

⚠️ **A Melhorar:**
- [ ] Implementar criptografia para credenciais
- [ ] Adicionar suporte a Azure Key Vault
- [ ] Implementar logs auditados de acesso ao banco

## Checklist para Contribuidores

Antes de fazer commit:

1. ✅ Nunca commitar o arquivo `.env`
2. ✅ Manter `.env.example` atualizado (sem valores reais)
3. ✅ Adicionar novas variáveis sensíveis ao `.env.example` (com placeholder)
4. ✅ Documentar todas as variáveis de ambiente necessárias

## Referências

- [DotNetEnv - GitHub](https://github.com/tomasmcguinness/dotnetenv)
- [Microsoft - Secrets Manager Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/secure-net-microservices-web-applications/)
- [OWASP - Secrets Management](https://owasp.org/www-community/attacks/Sensitive_Data_Exposure)
