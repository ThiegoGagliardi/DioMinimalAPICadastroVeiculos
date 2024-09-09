# Minimal API com C# .NET, EF Core, MySQL, JWT e Testes Unitários

Este projeto é uma aplicação API minimalista construída em C# com .NET 8, utilizando Entity Framework Core para acesso ao banco de dados MySQL. A API fornece endpoints para o cadastro de usuários e veículos, com controle de autenticação e autorização via tokens JWT. Testes unitários também são implementados para garantir a qualidade do código.

## Funcionalidades

- **Autenticação JWT**: Autenticação de usuários com JWT Bearer Token.
- **Controle de Autorização**: Endpoints protegidos para operações de criação, leitura, atualização e exclusão de usuários e veículos.
- **Entity Framework Core**: ORM para manipulação dos dados armazenados em um banco de dados MySQL.
- **Cadastro de Usuários**: Criação de novos usuários e login para obtenção de tokens JWT.
- **Cadastro de Veículos**: CRUD para veículos, associando-os a usuários.
- **Testes Unitários**: Validação da lógica de negócio e das operações da API.

## Tecnologias Utilizadas

- **C# .NET 7**
- **Entity Framework Core**
- **MySQL**
- **JWT (JSON Web Token)**
- **MsTest** (para testes unitários)

## Configuração do Projeto

### Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [MySQL](https://www.mysql.com/downloads/)
- Ferramenta de gerenciamento de API (ex: Postman) para testar os endpoints

### Passos para executar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/nome-do-projeto.git
   cd nome-do-projeto
   ```

2. Configure o banco de dados MySQL em `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=minimal_api_cadastro_carros;User ID=;Password=;"
   }
   ```

3. Aplique as migrações para o banco de dados:
   ```bash
   dotnet ef database update
   ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

5. A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

### Endpoints

- **Autenticação**
  - `POST /login`: Faz login e retorna um token JWT.
  
- **Usuários**
  - `Post /administradores`: inclui um novo usuário. (Autenticação necessária)
  - `GET /administradores/{id}`: Retorna um usuário específico. (Autenticação necessária)
  - `PUT /administradores/{id}`: Atualiza os dados de um usuário. (Autenticação e Autorização necessárias)
  - `DELETE /administradores/{id}`: Remove um usuário. (Autenticação e Autorização necessárias)

- **Veículos**
  - `Post /veiculos`: Retorna a lista de veículos. (Autenticação necessária)
  - `GET /veiculos/{id}`: Retorna um veículo específico. (Autenticação necessária)
  - `POST /veiculos`: Cadastra um novo veículo. (Autenticação necessária)
  - `PUT /veiculos/{id}`: Atualiza os dados de um veículo. (Autenticação necessária)
  - `DELETE /veiculos/{id}`: Remove um veículo. (Autenticação necessária)

### Autenticação

A autenticação é feita utilizando JWT (JSON Web Tokens). Após se registrar ou fazer login, o usuário recebe um token JWT, que deve ser enviado no cabeçalho das requisições subsequentes:

```bash
Authorization: Bearer {token}
```

### Testes Unitários

Os testes unitários são realizados utilizando o MSTest. Para executar os testes:

```bash
dotnet test
```

### Estrutura do Projeto

- `Controllers/`: Contém os controladores da API.
- `Models/`: Contém os modelos de domínio.
- `Data/`: Configuração do Entity Framework Core e o contexto do banco de dados.
- `Services/`: Contém a lógica de negócio, como autenticação e autorização.
- `Tests/`: Testes unitários implementados utilizando MSTest.

