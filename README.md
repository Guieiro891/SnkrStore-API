# Snkr Store 👟

Loja virtual de tênis desenvolvida como projeto acadêmico e de portfólio. A aplicação é composta por uma **API REST em C# (.NET)** e um **app mobile em Flutter**, integrados a um banco de dados **SQL Server**.

## 🚀 Tecnologias Utilizadas

### Back-end
- **C# / .NET 10**
- **ASP.NET Core Web API**
- **Entity Framework Core** (Code-First)
- **SQL Server**
- **BCrypt.Net** (hash de senhas)
- **Data Annotations** (validações)

### Front-end Mobile
- **Flutter / Dart**
- **HTTP package** (consumo da API)

## ✨ Funcionalidades

- Cadastro e autenticação de clientes (login com verificação via BCrypt)
- Listagem de tênis por marca, tamanho e estoque
- Fluxo completo de compra: criação de pedido, item, cálculo de total e finalização
- Baixa automática de estoque ao comprar
- Histórico de pedidos do cliente
- Validação de dados de entrada (Data Annotations)
- CRUD completo de todas as entidades

## 🏗️ Estrutura do Projeto
SnkrStore/
├── SnkrStore.Domain/ # Entidades e enums do domínio
├── SnkrStore.API/ # API REST (controllers, DbContext, DTOs)
└── snkr_store_app/ # App Flutter


## 🗄️ Entidades do Domínio

- **Marca** — fabricante do tênis (Nike, Adidas, etc.)
- **Tenis** — produto, com modelo, tamanho, cor, preço e estoque
- **Cliente** — dados do comprador e credenciais
- **Endereco** — endereço de entrega do cliente
- **Pedido** — registro da compra, com status, frete e desconto
- **ItemPedido** — item individual vinculado a um pedido
- **Pagamento** — forma e status do pagamento

## 🔌 Endpoints Principais

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/Cliente/login` | Autenticação do cliente |
| GET | `/api/Tenis` | Lista todos os tênis |
| GET | `/api/Tenis/{id}` | Busca tênis por ID |
| POST | `/api/Pedido` | Cria um novo pedido |
| POST | `/api/Pedido/{id}/finalizar` | Calcula total e finaliza o pedido |
| POST | `/api/ItemPedido` | Adiciona item (com baixa de estoque) |
| GET | `/api/Pedido/cliente/{clienteId}` | Histórico de pedidos do cliente |

## ⚙️ Como Rodar o Projeto

### Pré-requisitos
- .NET 10 SDK
- SQL Server
- Flutter SDK
- Visual Studio / VS Code

### Back-end (API)
```bash
cd SnkrStore.API
dotnet restore
dotnet ef database update
dotnet run
```

---

Desenvolvido por [Felipe Guieiro](https://github.com/Guieiro891)