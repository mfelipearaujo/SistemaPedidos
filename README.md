# SistemaPedidos

![Captura de tela](assets/captura-de-tela.png)

---

## Introdução

SistemaPedidos é uma API Restful em ASP.NET com Clean Architecture, que gerencia clientes, produtos e pedidos. O sistema está organizado em camadas, usa os padrões Repository e Unit of Work para abstração e controle do acesso a dados, utiliza DTOs para transferência de dados e AutoMapper para mapeamento entre entidades e DTOs. O banco utilizado é PostgreSQL, acessado via Entity Framework Core. Tanto a aplicação quanto o banco de dados rodam em containers Docker, orquestrados via Docker Compose.

---

## Arquitetura Geral

-   **Clean Architecture**  
    Separação clara das responsabilidades em camadas:
    -   Domain: entidades e regras de negócio
    -   Application: DTOs e lógica de aplicação
    -   Infrastructure: acesso a dados, implementação dos repositórios e Unit of Work
    -   API: camada de apresentação e controllers

-   **Repository Pattern**  
    O acesso a dados é abstraído por repositórios, promovendo baixo acoplamento entre as camadas e facilitando a manutenção e testes.

-   **Unit of Work Pattern**  
    O padrão Unit of Work coordena as operações de múltiplos repositórios sob uma única transação, garantindo atomicidade e consistência nas operações de escrita.

-   **Containers Docker**  
    A aplicação e o banco de dados PostgreSQL são executados em containers Docker, facilitando o setup, a portabilidade e o isolamento do ambiente. O Docker Compose é utilizado para orquestrar ambos os serviços simultaneamente.

---

## Endpoints da API

### Cliente

| Método | Endpoint              | Descrição                |
| ------ | --------------------- | ------------------------ |
| GET    | /api/v1/clientes      | Listar todos os clientes |
| GET    | /api/v1/clientes/{id} | Consultar cliente por ID |
| POST   | /api/v1/clientes      | Criar novo cliente       |
| PUT    | /api/v1/clientes/{id} | Atualizar cliente        |
| DELETE | /api/v1/clientes/{id} | Remover cliente          |

### Produto

| Método | Endpoint              | Descrição                |
| ------ | --------------------- | ------------------------ |
| GET    | /api/v1/produtos      | Listar todos os produtos |
| GET    | /api/v1/produtos/{id} | Consultar produto por ID |
| POST   | /api/v1/produtos      | Criar novo produto       |
| PUT    | /api/v1/produtos/{id} | Atualizar produto        |
| DELETE | /api/v1/produtos/{id} | Remover produto          |

### Pedido

| Método | Endpoint             | Descrição                   |
| ------ | -------------------- | --------------------------- |
| GET    | /api/v1/pedidos      | Listar todos os pedidos     |
| GET    | /api/v1/pedidos/{id} | Consultar pedido por ID     |
| POST   | /api/v1/pedidos      | Criar novo pedido com itens |
| PUT    | /api/v1/pedidos/{id} | Atualizar pedido existente  |
| DELETE | /api/v1/pedidos/{id} | Remover pedido por ID       |

---

## Requisitos

-   [.NET 9 SDK](https://dotnet.microsoft.com/pt-br/download) (para rodar testes e executar localmente)
-   [Docker](https://docs.docker.com/get-docker/) (para rodar a API e banco via containers)
-   [Docker Compose](https://docs.docker.com/compose/) (para orquestrar múltiplos containers)

---

## Instruções para rodar o projeto

1. Certifique-se de que o Docker está instalado e em execução em sua máquina.

2. Clone o repositório:

```bash
git clone https://github.com/mfelipearaujo/SistemaPedidos.git
```

3. Acesse o diretório do projeto:

```bash
cd SistemaPedidos
```

4. Inicie os containers da API e do banco PostgreSQL simultaneamente usando Docker Compose:

```bash
docker-compose up --build
```

5. A API estará disponível em http://localhost:5050/swagger/index.html

6. O banco PostgreSQL estará rodando no container, na porta padrão 5432 (não exposta para o host).

---

## Criar e aplicar migrations (localmente)

Certifique-se de estar no diretório raiz da aplicação (`/SistemaPedidos`).

1. Instale o Entity Framework Core tools:

```bash
dotnet tool install --global dotnet-ef
```

2. Crie uma nova migration chamada InitialCreate:

```bash
dotnet ef migrations add InitialCreate --project SistemaPedidos.Infrastructure/SistemaPedidos.Infrastructure.csproj --startup-project SistemaPedidos.API/SistemaPedidos.API.csproj
```

3. Aplique a migration ao banco de dados:

```bash
dotnet ef database update --project SistemaPedidos.Infrastructure/SistemaPedidos.Infrastructure.csproj --startup-project SistemaPedidos.API/SistemaPedidos.API.csproj
```

---

## Executar testes (localmente)

Certifique-se de ter o .NET SDK instalado.

1. Acesse o diretório do projeto de testes:

```bash
cd Tests/SistemaPedidos.Domain.Tests
```

2. Execute os testes:

```bash
dotnet test
```
