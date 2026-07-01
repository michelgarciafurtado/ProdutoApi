# Estudo de Caso: Comunicação Assíncrona com RabbitMQ e .NET 10

Este documento especifica a arquitetura e os requisitos técnicos para a implementação de um estudo de caso focado em comunicação assíncrona orientada a eventos, integrando serviços hospedados e mensageria no ecossistema .NET.

---

## 1. Introdução

O cenário proposto consiste em dois microsserviços com responsabilidades distintas e desacopladas que devem ser executadas juntas apesar de estarem em repositórios diferentes com dependência do RabbitMQ. Para instruções próprias verifique a seção 5 deste projeto:
* **ProdutosAPI:** Provedor de dados com persistência em banco de dados relacional.
* **CardapioOnline:** Consumidor que mantém dados estritamente em memória.

A comunicação entre os serviços é realizada de forma assíncrona, utilizando o **RabbitMQ** como message broker e o **MassTransit** como biblioteca de abstração para o barramento de mensageria no .NET 10. O foco do projeto é puramente didático, demonstrando padrões de sincronização ativa, passiva e tarefas temporizadas em segundo plano.

---

## 2. Objetivos

### 2.1. Objetivos Didáticos
* **Configuração de Mensageria:** Demonstrar a configuração prática de produtores (*Publishers*) e consumidores (*Consumers*) usando filas específicas.
* **Mapeamento de Entidades:** Exibir como realizar o acoplamento (*bind*) de mensagens baseado no nome da entidade de contrato.
* **Ciclo de Vida Nativo:** Demonstrar a inicialização automática de rotinas na subida do sistema sem intervenção do usuário via `IHostedService`.
* **Rotinas Agendadas:** Demonstrar a execução de tarefas temporizadas recorrentes em segundo plano utilizando `BackgroundService`.

### 2.2. Objetivos de Negócio (Funções Pretendidas)
* **Sincronização Inicial:** Ao iniciar, o *CardapioOnline* deve solicitar e carregar ativamente todos os produtos cadastrados na *ProdutosAPI*.
* **Atualização em Tempo Real:** Sempre que um novo produto for criado na *ProdutosAPI*, o *CardapioOnline* deve receber a notificação e atualizar sua memória instantaneamente.
* **Consistência Recorrente:** Garantir que o cardápio em memória seja atualizado periodicamente para evitar divergências com o banco de dados.

---

## 3. Dependências

### 3.1. Infraestrutura
* **Banco de Dados:** Microsoft SQL Server (Utilizado exclusivamente pela *ProdutosAPI*).
* **Message Broker:** RabbitMQ versão 4.x (Gerenciado via Docker).

Para subir o broker localmente, execute o comando abaixo:

```bash
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management
```

### 3.2. Pacotes Nuget (.NET 10)
* **Comuns aos dois projetos:**
  * `MassTransit.RabbitMQ` (Abstração e transporte da mensageria).
* **ProdutosAPI:**
  * `Microsoft.EntityFrameworkCore.SqlServer` (Para persistência do CRUD básico).
* **CardapioOnline:**
  * `Microsoft.Extensions.Hosting` (Suporte nativo ao `IHostedService` e `BackgroundService`).

---

## 4. Requisitos Técnicos

### 4.1. Arquitetura de Comunicação (MassTransit)
* **Abordagem Ativa (Event-Driven):** A *ProdutosAPI* publica o evento `ProdutoCriadoEvent` no barramento após salvar no banco. O *CardapioOnline* assina esse evento para atualizar sua lista em memória.
* **Abordagem Passiva (Request/Response via Bus):** O *CardapioOnline* envia a mensagem `ObterTodosProdutosRequest` ao iniciar, e a *ProdutosAPI* responde com a lista atual através do barramento do MassTransit.

### 4.2. Implementação dos Serviços em Segundo Plano
* **Carga Inicial via `IHostedService`:** Implementado no *CardapioOnline*. No método `StartAsync`, utiliza o `IRequestClient<ObterTodosProdutosRequest>` do MassTransit para buscar a massa de dados inicial antes de liberar o fluxo total do app.
* **Sincronização Recorrente via `BackgroundService`:** Implementado no *CardapioOnline*. Possui um loop contínuo (`while (!stoppingToken.IsCancellationRequested)`) executado de maneira temporizada para solicitar o reenvio da lista completa de produtos.

### 4.3. Restrições (Fora de Escopo)
* Detalhes de Id e Log.
* Persistência de dados ou bancos de dados no microsserviço *CardapioOnline*.
* Requisitos de dados e validações complexas.
* Requisitos de autenticação via Token.
* Rastreabilidade de requisições (*Tracing*).

## 5. Subindo o Projeto (Passos Iniciais)

Siga os passos abaixo para configurar o ambiente e executar os microsserviços localmente.

### 5.1. Pré-requisitos
Antes de iniciar, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:
* [Docker](https://docker.com) (Motor de execução de containers)
* [Docker Desktop](https://docker.comproducts/docker-desktop/) (Interface de gerenciamento para Windows/Mac)
* [Microsoft SQL Server](https://microsoft.com) (Instância local ou via container para a base de dados)

### 5.2. Inicialização do RabbitMQ 4.x
Execute o comando abaixo no seu terminal para subir o container oficial do RabbitMQ versão 4 com o painel de gerenciamento ativado:

```bash
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management
```
> **Nota:** O painel administrativo do RabbitMQ estará disponível em `http://localhost:15672` (Usuário/Senha padrão: `guest`).

### 5.3. Configuração dos Repositórios
1. Baixe os repositórios dos projetos para a sua máquina local:
   * **ProdutosAPI**
   * **CardapioOnline**

2. Abra o terminal na raiz do projeto **ProdutosAPI** para aplicar as tabelas iniciais no banco de dados através do Entity Framework Core:

```bash
dotnet ef migrations add FirstCreate
dotnet ef database update
```

> **Dados Iniciais (Seed):** A tabela de categorias será populada automaticamente na primeira execução através do mecanismo de semente (*Seed Data*), disponibilizando as seguintes opções:
> * `1` - Lanches
> * `2` - Porções
> * `3` - Bebidas

### 5.4. Execução e Testes
Esteja certo que o RabbitMQ esta rodando antes de executar os projetos.
Após rodar os projetos (`dotnet run`), você poderá interagir com a aplicação ProdutosAPI de duas formas:
1. **ProdutosAPI - Interface do Swagger:** Acesse pelo navegador através do endereço `http://localhost:<Port_Number>/swagger/index.html` para testar os endpoints do CRUD de produtos.
2. **ProdutosAPI - Arquivo HTTP:** Se preferir, utilize o arquivo `ProdutosApi.http` diretamente no Visual Studio ou VS Code para disparar as requisições de teste.
3. **CardapioOnline - Página html**: ao rodar este projeto a pagina subira e mostrara as alterações de produtos a cada e 2 minutos ou toda vez que um produto for alterado
