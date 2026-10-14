# Gerenciador de Produtos

Trabalho Prático 03 da disciplina CBTSWE2 (ADS 671) — IFSP Campus Cubatão.

Sistema de gerenciamento de produtos em **ASP.NET Core MVC** com **Entity Framework Core** e banco **SQLite**.

## Funcionalidades

- Listagem de produtos na página inicial
- Cadastro, edição e exclusão de produtos
- Página de detalhes do produto
- Validação de dados no servidor e no navegador
- Interface com Bootstrap 5

## Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- Acesso à internet (Bootstrap e jQuery são carregados por CDN)

## Como executar

```
git clone <url-do-repositorio>
cd GerenciadorProdutos
dotnet restore
dotnet run
```

Acesse http://localhost:5080. O banco `produtos.db` é criado automaticamente na primeira execução, com três produtos de exemplo.

## Estrutura

```
Controllers/ProdutosController.cs   camada de controle (CRUD)
Models/Produto.cs                   entidade e validações
Data/AppDbContext.cs                mapeamento do Entity Framework
Views/Produtos/                     Index, Details, Create, Edit, Delete
Views/Shared/                       layout e scripts de validação
wwwroot/                            estilos e validação client-side
```

## Autores

Preencher com os nomes da dupla.
