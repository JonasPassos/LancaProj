# LancaProj

Projeto backend desenvolvido em C# com ASP.NET Core para gerenciamento simples de projetos.

## Descrição

Este projeto oferece uma API RESTful para cadastro, edição, listagem e exclusão de projetos. Utiliza Entity Framework Core para
persistência dos dados em banco relacional e segue boas práticas para organização do código.

---

## Funcionalidades

- CRUD completo para projetos (`Projeto`): criar, listar, atualizar e excluir  
- Modelagem simples com propriedades básicas (Nome, Descrição, Data de Criação)  
- Validação básica dos dados  
- Uso de Entity Framework Core para manipulação do banco de dados  
- Padrão Repository para abstração da persistência (interface e implementação)  
- Serviço dedicado para lógica de negócio  
- Controller para expor endpoints RESTful  
- Mapeamento simples com AutoMapper (se presente no código)  

---

## Tecnologias utilizadas

- C#  
- ASP.NET Core Web API  
- Entity Framework Core (Code First)  
- SQL Server 
- Visual Studio / VS Code  
- Git

---

## Como executar o projeto

### Pré-requisitos

- [.NET 7 SDK](https://dotnet.microsoft.com/download)  
- Banco de dados SQL Server ou compatível  
- Visual Studio 2022 ou VS Code

### Passos

1. Clone o repositório:

```bash
git clone https://github.com/JonasPassos/LancaProj.git
