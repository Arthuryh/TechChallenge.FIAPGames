# 🎮 FIAP Games API

API desenvolvida para o Tech Challenge da FIAP, focada no gerenciamento de jogos, autenticação, promoções e biblioteca de usuários utilizando princípios de DDD e arquitetura em camadas.

## 🚀 Tecnologias

* ⚙️ .NET 8
* 🗄️ Entity Framework Core
* 🐬 MySQL
* 🔐 JWT Authentication
* 📦 Dapper
* 🧪 xUnit + Moq
* 📄 Swagger


## 🏗️ Arquitetura

O projeto segue separação em camadas:

```bash
📁 Domain          → Entidades e regras de negócio
📁 Application     → Casos de uso e serviços
📁 Infrastructure  → Banco de dados, repositórios e integrações
📁 WebApi          → Controllers e endpoints HTTP
```

## ✨ Funcionalidades

* 👤 Cadastro e autenticação de usuários
* 🎮 CRUD de jogos
* 📚 Biblioteca de jogos do usuário
* 💸 Sistema de promoções
* 🔐 Autorização com JWT e Roles
* 🧪 Testes unitários
* 📑 Swagger para documentação da API
* 📝 Middleware global para tratamento de erros e logs

## ▶️ Como executar

### Clone o projeto

```bash
git clone https://github.com/Arthuryh/TechChallenge.FIAPGames.git
```

### Configure o banco

Atualize a connection string no `appsettings.json`.

O sistema se encarrega da primeira carga

```bash
dotnet run
```

## 📌 Swagger

Acesse:

```bash
https://localhost:xxxx/swagger
```

## 🧪 Executar testes

```bash
dotnet test
```

## 🔒 Autenticação

A API utiliza JWT Bearer Token.

Exemplo de header:

```http
Authorization: Bearer seu_token
```

## 📚 Conceitos aplicados

* ✅ DDD
* ✅ Repository Pattern
* ✅ Dependency Injection
* ✅ Middleware Pipeline
* ✅ Clean Code
* ✅ SOLID

## 👨‍💻 Objetivo

Projeto acadêmico desenvolvido no Tech Challenge da FIAP com foco em boas práticas de arquitetura, organização de código e construção de APIs modernas em .NET.
