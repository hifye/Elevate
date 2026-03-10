# ElevateApi - Guia Técnico e Arquitetural 🏗️

Este documento foi criado para fornecer uma visão profunda sobre as decisões de design, padrões de arquitetura e tecnologias implementadas no **ElevateApi**. Se você é um recrutador ou desenvolvedor interessado em como este projeto foi construído, este guia é para você.

---

## 🏛️ Arquitetura: Clean Architecture

O projeto segue os princípios da **Arquitetura Limpa**, com o objetivo de separar as preocupações e garantir que a lógica de negócio (o "coração" da aplicação) seja independente de frameworks, bancos de dados ou interfaces externas.

### Camadas do Sistema:
- **Domain (Núcleo):** Contém as entidades de negócio, objetos de valor (`Value Objects`) e lógica puramente de domínio. É a camada mais interna e não possui dependências externas.
- **Application:** Onde residem os casos de uso. Implementa o padrão **CQRS** com **MediatR**. Aqui definimos as interfaces que a infraestrutura deve implementar, garantindo o desacoplamento.
- **Infrastructure:** Responsável pelo acesso a dados (PostgreSQL + Dapper), segurança (JWT), e serviços externos.
- **Presentation (API):** Ponto de entrada da aplicação, focado em expor os endpoints e gerenciar as requisições HTTP.

---

## 🚀 Padrões e Práticas de Desenvolvimento

### 1. CQRS (Command Query Responsibility Segregation)
Utilizamos o **MediatR** para separar as operações de escrita (**Commands**) das operações de leitura (**Queries**).
- **Vantagem:** Permite otimizar as consultas de forma independente da lógica de alteração de dados, além de simplificar os controladores da API.

### 2. Result Pattern & Functional Error Handling
Em vez de depender de exceções para controlar o fluxo de negócio (o que pode ser custoso em termos de performance), utilizamos o **Result Pattern** (`Result.cs`).
- Isso torna o código mais previsível e fácil de testar, forçando o tratamento de cenários de sucesso e falha de forma explícita.
- Implementamos métodos como `Bind` e `Map` para permitir um encadeamento funcional das operações.

### 3. Validação com FluentValidation & Pipeline Behaviors
As validações de entrada são separadas da lógica de negócio. Utilizamos **Validation Behaviors** no pipeline do MediatR.
- Toda requisição que chega passa automaticamente por um validador antes de atingir o Handler. Se houver falhas, uma exceção de validação é lançada e capturada globalmente.

### 4. Alta Performance com Dapper
Optamos pelo **Dapper** em vez de um ORM pesado (como EF Core) para as consultas.
- **Por que?** O Dapper oferece uma performance próxima ao ADO.NET puro, permitindo total controle sobre o SQL executado.
- As consultas SQL são centralizadas em classes constantes (`CourseSql.cs`), facilitando a manutenção e o tuning.

### 5. Unit of Work & Transacionalidade
Garantimos a integridade dos dados através do padrão **Unit of Work**.
- Todas as operações dentro de um mesmo caso de uso compartilham a mesma transação de banco de dados, sendo confirmadas (`Commit`) apenas se tudo ocorrer com sucesso.

### 6. Guard Clauses
Utilizamos classes de proteção (**Guard Clauses**) para validar pré-condições em entidades e objetos de valor, evitando que o sistema entre em um estado inválido.

---

## 🛡️ Segurança e Observabilidade

- **JWT (JSON Web Token):** Autenticação robusta com tokens de acesso e suporte a autorização baseada em roles.
- **Logging Estruturado:** Com **Serilog**, capturamos logs ricos em detalhes (incluindo MachineName, ThreadId e contextos de erro) salvos em arquivos rotativos e console.
- **Global Exception Handling:** Um middleware centralizado captura erros não tratados e retorna respostas padronizadas ao cliente, evitando vazamento de informações sensíveis.

---

## 🧠 Diferenciais Técnicos

- **C# 13 & .NET 9:** Uso de recursos modernos da linguagem para um código mais limpo e performático.
- **Separation of Concerns:** Cada classe tem uma responsabilidade única e bem definida.
- **Testabilidade:** A arquitetura permite mockar facilmente as dependências de infraestrutura para testes unitários da lógica de aplicação.

---

## 📩 Contato

Se você gostou da abordagem técnica deste projeto e deseja discutir oportunidades ou colaborações, sinta-se à vontade para entrar em contato através do meu perfil no GitHub ou LinkedIn.
