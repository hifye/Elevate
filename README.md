# Elevate

Elevate é uma **API REST construída em .NET** que simula um **sistema de gestão de cursos e inscrições**, ideal como projeto de referência para boas práticas de arquitetura e desenvolvimento de software escalável.

O projeto foi desenvolvido para demonstrar como criar **APIs robustas e organizadas**, aplicando **Clean Architecture**, **CQRS**, integração com bancos de dados e padrões modernos de desenvolvimento.

---

## 🚀 Funcionalidades do projeto

Elevate modela funcionalidades típicas de uma plataforma de aprendizado online:

- **Gerenciamento de cursos**: criar, listar, atualizar e excluir cursos.  
- **Gestão de instrutores**: adicionar e consultar instrutores, vinculando-os aos cursos.  
- **Inscrições de alunos**: cadastrar alunos, realizar inscrições em cursos e acompanhar status.  
- **Autenticação e segurança**: controle de acesso via **JWT**, garantindo que apenas usuários autorizados possam realizar operações sensíveis.  
- **Validações consistentes**: uso de **FluentValidation** e **Pipeline Behaviors** para assegurar integridade dos dados e tratamento previsível de erros.  
- **APIs escaláveis e performáticas**: implementação de **Clean Architecture** e **CQRS** para separar leitura e escrita, garantindo organização e manutenibilidade do código.  

⚠️ O Elevate é voltado para **aprendizado e referência técnica**, não sendo um produto pronto para produção.

---

## 🧠 Propósito

Este projeto serve para:

- Desenvolvedores que querem entender e aplicar **arquitetura limpa em .NET**.  
- Times que precisam de um **template de projeto inicial** para APIs REST escaláveis.  
- Pessoas que querem estudar **CQRS, validação pipeline e autenticação JWT** em um contexto de negócios realista.  

---

## 🛠️ Tecnologias principais

- **.NET 9 / C# 13**  
- **Clean Architecture / CQRS / MediatR**  
- **Dapper / PostgreSQL / SQL Server**  
- **JWT Authentication**  
- **FluentValidation & Pipeline Behaviors**  
- **APIs REST / Integração de sistemas distribuídos**  
- **Serilog para logging estruturado**

---

## 🧩 Estrutura do projeto

- `Domain` – modelos, regras de negócio e objetos de valor  
- `Application` – casos de uso, comandos e consultas  
- `Infrastructure` – acesso a dados e serviços externos  
- `Presentation` – API e endpoints

---

## ▶️ Como rodar

> **Pré-requisitos:** .NET SDK 9, Docker (opcional), PostgreSQL configurado

1. Clone o repositório  
   ```bash
   git clone https://github.com/hifye/Elevate.git
   cd Elevate
