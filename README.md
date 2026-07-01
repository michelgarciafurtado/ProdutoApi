## Estudo de Caso

## 📝 - Briefing

Realizar um estudo de caso que demonstre comunicação assincrona com RabbitMQ, .Net, Banco de dados e requisições temporizadas.

📋 **- Objetivos didáticos**

1. Demonstrar a configuração de Publisher/Consumer(com fila especifica);
2. Demonstrar como fazer o bind por nome de entidade;
3. Demonstrar a utilização de IHostedService para realização de tarefas sem interação do usuário quando o sistema é startado;
4. Demonstrar a utilização de BackGroundService para realização de tarefas temporaizadas

## 🖼️ - Cenário Proposto

Um cardapioOnline que é alimentado por uma API de Produtos com comunicação assíncrona uasndo RabbitMQ implementada através de MassTransit. O CardapioOnline não tem banco de dados, os produtos ficam somente em memoria, o Banco de dados SQLServer é acessado somente por ProdutosAPI que realiza um Crud básico no banco.

## 🎯 **- Objetivos Técnicos**

1. Realizar comunicação assincrona de maneira **ativa** entre publisher/consumer quando do evento de criação de um produto;
2. Realizar comunicação assincrona de maneira **passiva** entre publisher/consumer atraves de request quando do evento de subir aplicação;
3. Realizar comunicação assincrona de maneira **passiva recorrente (temporizada)** entre publisher/consumer de meneira temporizada com reenvio de lista de produtos

#### 🛠️ - Funções pretendidas:

1. Ao iniciar aplicação CardapioOnline faz um request para a api ProdutosAPI e carrega todos os produtos cadastrados;
2. Ao cadastrar um novo produto ele é publicado no CardapioOnline.

#### 🛑 - Não estão no escopo do projeto

1. Detalhes de Id e Log, persistência de dados;
2. Requisitos de dados;
3. Requisitos de autenticação via Token;
4. Rastreabilidade de requisições.

🏗️ **- Requisitos Técnicos**

1. Banco de dados SQLServer;
2. RabbitMQ versão 4 ;

```jsx
# latest RabbitMQ 4.x
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management
```

 

1. .Net versão 10.
