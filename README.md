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
- **System.Text.Json** (serialização e ocultação de campos sensíveis)

### Front-end Mobile
- **Flutter / Dart**
- **HTTP package** (consumo da API)

## ✨ Funcionalidades

### API
- CRUD completo de todas as entidades (Marcas, Tênis, Clientes, Endereços, Pedidos, Itens de Pedido e Pagamentos)
- Autenticação de cliente com verificação via BCrypt
- Ocultação de senha nas respostas da API (JsonIgnore)
- Validação automática de dados de entrada (Data Annotations)
- Regras de negócio:
  - Baixa automática de estoque ao comprar
  - Ajuste de estoque ao editar/remover itens de pedido
  - Cálculo de valor total (subtotal - desconto + frete)
  - Registro de pagamento ao finalizar a compra
- Endpoint para listar pedidos por cliente

### App Mobile
- Tela de login
- Tela de cadastro de cliente
- Listagem de tênis com modelo, tamanho, cor, estoque e preço
- Botão de compra por produto
- Tela "Meus Pedidos" com histórico
- Logout
- Nome do cliente exibido na barra superior

## 🏗️ Estrutura do Projeto

```
SnkrStore/
├── SnkrStore.Domain/   # Entidades e enums do domínio
├── SnkrStore.API/      # API REST (controllers, DbContext, DTOs)
└── snkr_store_app/     # App Flutter
```

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
| POST | /api/Cliente | Cadastra um novo cliente |
| POST | /api/Cliente/login | Autenticação do cliente |
| GET | /api/Tenis | Lista todos os tênis |
| GET | /api/Tenis/{id} | Busca tênis por ID |
| POST | /api/Pedido | Cria um novo pedido |
| POST | /api/Pedido/{id}/finalizar | Calcula total e finaliza o pedido |
| POST | /api/ItemPedido | Adiciona item (com baixa de estoque) |
| POST | /api/Pagamento | Registra o pagamento |
| GET | /api/Pedido/cliente/{clienteId} | Histórico de pedidos do cliente |

## ⚙️ Como Rodar o Projeto

### Pré-requisitos
- .NET 10 SDK
- SQL Server
- Flutter SDK
- Visual Studio / VS Code

### Back-end (API)

```
cd SnkrStore.API
dotnet restore
dotnet ef database update
dotnet run
```

A API ficará disponível em `http://localhost:5000`. O Swagger pode ser acessado em `/swagger`.

### Front-end (Flutter)

```
cd snkr_store_app
flutter pub get
flutter run
```

## 📌 Status do Projeto

✅ Funcionalidades principais implementadas e testadas:
- Fluxo completo de compra (pedido → item → finalização → pagamento)
- Baixa e devolução automática de estoque
- Cadastro, login e logout de clientes
- Histórico de pedidos
- Segurança: senhas com hash BCrypt e ocultação nas respostas

🚧 Próximos passos:
- Cancelamento de pedido (com devolução de estoque)
- Endereço vinculado ao pedido
- Imagens dos produtos
- Deploy na nuvem



Desenvolvido por [Felipe Guieiro](https://github.com/Guieiro891)
