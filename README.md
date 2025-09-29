# Integrantes

- Alexsandro Macedo: RM557068
- Leonardo Faria Salazar: RM557484
- Guilherme Felipe da Silva Souza: RM558282

# **Eficientiza API**

## Domínios

Estação: Utilizado para o controle do pátio com caracteristicas como nome, localização, capacidade, etc

Usuário: Utilizado para o controle de usuários do sistema, nele será cadastrado tanto funcionários quanto usuários finais do aplicativo

Moto: Utilizado para cadastro de motos, será necessário para futuramente ser feito um controle de entrada e saída motos, com isso precisamos identificar cada moto dentro do nossos sistema

## Justificativa da Arquitetura

Este projeto foi desenvolvido utilizando a arquitetura **Clean Architecture** com **Domain Driven Design (DDD)**. O objetivo foi criar uma API escalável, mantendo a lógica de negócios e os dados separados, garantindo que a aplicação seja fácil de manter e evoluir ao longo do tempo.

### Arquitetura
A arquitetura do projeto é composta pelas seguintes camadas principais:

- **Controllers**: Responsáveis por lidar com as requisições HTTP, delegando a lógica de negócio para os serviços.
- **Services**: Implementam a lógica de negócio.
- **Repositories**: Gerenciam o acesso ao banco de dados utilizando **Entity Framework Core**.
- **Models**: Contêm as entidades e DTOs (Data Transfer Objects) que representam os dados da aplicação.
- **Data**: Contém o contexto de banco de dados e implementações específicas de repositórios.
- **Doc**: Contém exemplos de respostas para documentação da API com **Swagger**.
- **Mappers**: Contêm as funções de conversão entre **Entities** e **DTOs**.

Essa estrutura permite que a API seja bem organizada e modular, facilitando a manutenção e evolução.

---

## Instruções de Execução da API

### Pré-requisitos

- **.NET 6 ou superior** instalado. Você pode verificar sua versão com:
  ```bash
  dotnet --version
  ```

- **SQL Server** ou **Banco de Dados Relacional** para rodar a aplicação, ou se preferir, utilize um banco em memória (`InMemory`).

### Passos para Execução

1. **Clone o repositório**:
    ```bash
    git clone https://github.com/seu-usuario/eficientiza-api.git
    cd eficientiza-api
    ```

2. **Restaure as dependências do projeto**:
    ```bash
    dotnet restore
    ```

3. **Crie a Migration Inicial e aplique ao banco de dados**:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

    Caso utilize outro banco que não seja o **SQL Server**, altere a string de conexão no `appsettings.json`.

4. **Execute a aplicação**:
    ```bash
    dotnet run
    ```

    A API estará disponível em: `http://localhost:5000` (ou `https://localhost:5001` para HTTPS).

---

## Exemplos de Uso dos Endpoints

### **1. Listar Motos (GET /api/v1/moto)**

**URL**:
```bash
GET /api/v1/moto
```

**Descrição**: Retorna a lista de motos cadastradas na aplicação.

**Parâmetros de Query**:
- `PaginaAtual`: Página que será retornada. Padrão: `1`
- `LimitePagina`: Número de itens por página. Padrão: `10`

**Exemplo de Resposta (200 OK)**:
```json
{
  "data": [
    {
      "Placa": "ABC1D23",
      "Modelo": "Honda CG 160",
      "Cor": "Vermelha",
      "Ano": 2020,
      "Status": "Disponível",
      "links": {
        "self": "https://localhost:5001/api/v1/moto/1",
        "update": "https://localhost:5001/api/v1/moto/1",
        "delete": "https://localhost:5001/api/v1/moto/1"
      }
    }
  ],
  "links": {
    "self": "https://localhost:5001/api/v1/moto?PaginaAtual=1&LimitePagina=10",
    "create": "https://localhost:5001/api/v1/moto",
    "first": "https://localhost:5001/api/v1/moto?PaginaAtual=1&LimitePagina=10",
    "prev": null,
    "next": "https://localhost:5001/api/v1/moto?PaginaAtual=2&LimitePagina=10",
    "last": "https://localhost:5001/api/v1/moto?PaginaAtual=10&LimitePagina=10"
  },
  "pagina": {
    "PaginaAtual": 1,
    "TotalPaginas": 10,
    "TotalRegistros": 100
  }
}
```

### **2. Criar Moto (POST /api/v1/moto)**

**URL**:
```bash
POST /api/v1/moto
```

**Body** (Exemplo de request):
```json
{
  "Placa": "XYZ9H87",
  "Modelo": "Yamaha Fazer 250",
  "Cor": "Preta",
  "Ano": 2022,
  "Status": "Em manutenção"
}
```

**Exemplo de Resposta (201 Created)**:
```json
{
  "data": {
    "Placa": "XYZ9H87",
    "Modelo": "Yamaha Fazer 250",
    "Cor": "Preta",
    "Ano": 2022,
    "Status": "Em manutenção",
    "links": {
      "self": "https://localhost:5001/api/v1/moto/2",
      "update": "https://localhost:5001/api/v1/moto/2",
      "delete": "https://localhost:5001/api/v1/moto/2"
    }
  }
}
```

### **3. Atualizar Moto (PUT /api/v1/moto/{id})**

**URL**:
```bash
PUT /api/v1/moto/1
```

**Body** (Exemplo de request):
```json
{
  "Placa": "ABC1D23",
  "Modelo": "Honda CG 160",
  "Cor": "Azul",
  "Ano": 2020,
  "Status": "Indisponível"
}
```

**Exemplo de Resposta (200 OK)**:
```json
{
  "Placa": "ABC1D23",
  "Modelo": "Honda CG 160",
  "Cor": "Azul",
  "Ano": 2020,
  "Status": "Indisponível"
}
```

---

### **Observação**
- **Swagger** está habilitado por padrão para documentação de endpoints. Você pode acessar a documentação em `http://localhost:5000/swagger` (ou `https://localhost:5001/swagger` se estiver utilizando HTTPS).
