# SocialTransacoes
Simulação de gerenciamento de contas e pagamentos

Este projeto tem como propósito exercitar o gerenciamento de contas e transações bancárias de forma fictícia.
Ele foi desenvolvido em NetCore 3.1, utilizando bibliotecas do NuGet e se baseando em memória de um banco SQL Server.

Inicialmente é dividido nas pastas: Common, APIs, Services e Tests.

- Na pasta Common contém o projeto de infra (Contas). E também um projeto com os modelos utilizados.

- Na pasta APIs contém uma API para obter o token, e outra API RestFull que manipula contas e transações.

- Na pasta Services, contém um app console gerador randomico de contas e um projeto com os services utilizados pelas API RestFull.

- E por último, um projeto onde são executados os testes unitários e de integração.

- Foi integrado a API RestFull o SwaggerUI, que contém todas as chamadas disponíveis dos controllers de Contas e Transações.

- Esse projeto possue versionamento, e é demonstrado no controller de Contas, onde alguns EndPoints estão disponíveis na versão 1.0 e 2.0.
O controller Transacoes aceita as duas versões.Também foi possivel trabalhar com listas grandes, onde aplicamos paginação, filtros, e ordenação.

- Contém um CRUD de Contas, e operações comuns como saques, depósitos, transferências e pagamento de boletos com código de barras.
Além de poder consultar o saldo e extrato.

Abaixo segue as instruções de como rodar o projeto.

- Para obter token válido, é necessário rodar o projeto "Social.IdentidadeAPI".
No controlador AuthController contém os endPoints de criação e autenticação do usuário.

- Caso opte por um banco local. Basta descomentar a linha 21 da classe "namespace Social.IdentidadeAPI.Configuration.IdentityConfig".
Para rodar o migration desse projeto. É necessário abrir o Packege Maganer Console no Nuget Packege Maganer.
Executar o comando "update-database".

- Também tem anexado ao projeto, scripts de criação do banco principal.
Também será necessário descomentar a linha 21 da classe "namespace Social.RestFullAPI.Configuration.ApiConfig".

- Ordem de start:
	- Criar uma conta pelo endpoint "nova-conta".
	- Após obter token. Colocar como projeto padrão o "Social.RestFullAPI".
	- O ideal é configurar a solução para iniciar as duas APIs ao mesmo tempo. (Social.RestFullAPI e Social.IdentidadeAPI)
	- Rodar o projeto usando o lauchSettings "Docker" ou "IIS Express" em https://localhost:5001 ou http://localhost:5000.
	- Assim que o Swagger abrir via browser, a API poderá ser consumida.


