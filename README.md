API RESTful de Futebol
⚽ Visão Geral do Projeto

Esta é uma API de futebol construída em .NET 8 que consome dados da API TheSportsDB. A aplicação foi projetada para buscar informações de times e ligas, persistir esses dados em um banco de dados SQL Server e expor endpoints RESTful para consulta.
🚀 Como Rodar a Aplicação

Siga os passos abaixo para clonar o projeto e executá-lo em sua máquina local.
Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

    .NET 8 SDK

    SQL Server ou SQL Server Express

    SQL Server Management Studio (SSMS) ou Azure Data Studio

Configuração

    Clone o Repositório:

    git clone https://github.com/SEU_USUARIO/API-RESTful-de-futebol.git
    cd API-RESTful-de-futebol

    Substitua SEU_USUARIO pelo seu nome de usuário do GitHub.

    Configure a Conexão com o Banco de Dados:
    Abra o arquivo appsettings.json e atualize a DefaultConnection com as suas informações de conexão do SQL Server.

    "ConnectionStrings": {
      "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO;User Id=SEU_USUARIO;Password=SUA_SENHA;"
    }

    Aplique as Migrações do Banco de Dados:
    Use o Entity Framework Core para criar a tabela no seu banco de dados.

    dotnet ef database update

    Execute a Aplicação:

    dotnet run

    A API será iniciada e estará acessível em http://localhost:5142.

🌐 Documentação da API de Terceiros

Os dados desta API são obtidos e manipulados através da API gratuita da TheSportsDB. Você pode encontrar a documentação completa e informações sobre a chave da API em:

    TheSportsDB API Documentation: https://www.thesportsdb.com/api.php
