# Simulador-de-Encomendas-em-Drone

Este projeto foi desenvolvido como solução para o desafio técnico da dti digital.  
O objetivo é simular um sistema de entregas por drones em áreas urbanas, respeitando regras de capacidade, distância e prioridade, e alocar os pedidos da forma mais eficiente possível.

---

## Objetivo do Sistema

* Gerenciar pedidos com informações de localização (X, Y), peso e prioridade.
* Alocar pedidos em drones respeitando:
  * Capacidade máxima (kg)
  * Distância máxima (km)
  * Priorização de entregas mais urgentes
* Minimizar o número de viagens, buscando melhor aproveitamento da carga.

---

## Tecnologias Utilizadas

**Desenvolvimento**

* **.NET 8 / C#**: base para a lógica de negócio, escolhido pela robustez e performance.
* **ASP.NET Core Web API**: framework para construção dos endpoints RESTful.

**Dados**

* **MySQL**: banco de dados relacional para armazenar drones e pedidos.
* **Dapper**: micro ORM utilizado para acesso direto ao banco, garantindo consultas SQL nativas e eficientes.

**Controle de Versão e Colaboração**

* **Git** e **GitHub**: versionamento do código e publicação do projeto.

**Testes e Validação**

* **Postman**: utilizado para validar manualmente os endpoints, simular cenários de pedidos e avaliar a qualidade da alocação antes da entrega.

---

## Estrutura do Projeto

A API foi organizada em camadas para facilitar manutenção e escalabilidade:

# Simulador-de-Encomendas-em-Drone

Este projeto foi desenvolvido como solução para o desafio técnico da **dti digital**.  
O objetivo é simular um sistema de entregas por drones em áreas urbanas, respeitando regras de capacidade, distância e prioridade, e alocar os pedidos da forma mais eficiente possível.

---

## Objetivo do Sistema

* Gerenciar pedidos com informações de localização (X, Y), peso e prioridade.
* Alocar pedidos em drones respeitando:
  * Capacidade máxima (kg)
  * Distância máxima (km)
  * Priorização de entregas mais urgentes
* Minimizar o número de viagens, buscando melhor aproveitamento da carga.

---

## Tecnologias Utilizadas

**Desenvolvimento**

* **.NET 8 / C#**: base para a lógica de negócio, escolhido pela robustez e performance.
* **ASP.NET Core Web API**: framework para construção dos endpoints RESTful.

**Dados**

* **MySQL**: banco de dados relacional para armazenar drones e pedidos.
* **Dapper**: micro ORM utilizado para acesso direto ao banco, garantindo consultas SQL nativas e eficientes.

**Controle de Versão e Colaboração**

* **Git** e **GitHub**: versionamento do código e publicação do projeto.

**Testes e Validação**

* **Postman**: utilizado para validar manualmente os endpoints, simular cenários de pedidos e avaliar a qualidade da alocação antes da entrega.

---

## Estrutura do Projeto

A API foi organizada em camadas para facilitar manutenção e escalabilidade:

```text
Simulador_de_Encomendas_em_Drone/
│
├── Controllers/
│   ├── DroneController.cs       # Exposição dos endpoints
│   └── PedidoController.cs
│
├── DTOs/
│   ├── Drone/
│   │   └── CriaDroneDTO.cs
│   └── Pedido/
│       ├── CriaPedidoDTO.cs
│       └── LerPedidoDTO.cs
│
├── Models/
│   ├── Drone.cs
│   └── Pedido.cs
│
├── Repositories/
│   ├── DroneRepository.cs      # Acesso ao banco com Dapper
│   └── PedidoRepository.cs
│
├── Services/
│   ├── DroneService.cs         # Regras de negócio
│   └── PedidoService.cs
│
├── Program.cs
└── appsettings.json            # Configurações, incluindo conexão com banco
```
---

## Lógica de Alocação dos Pedidos

A regra principal de distribuição foi implementada no `PedidoService`.  
O algoritmo segue uma abordagem gulosa (greedy approach),é uma estratégia de solução de problemas que faz a escolha mais promissora em cada etapa, com o objetivo de alcançar a melhor solução global, mas sem retroceder ou revisar decisões passadas. Nesse caso, próxima ao "Best Fit Decreasing":

1. Ordena drones por maior capacidade de carga.
2. Ordena pedidos por prioridade e, em seguida, por peso.
3. Para cada pedido:
   * Verifica quais drones conseguem carregá-lo e alcançar o destino.
   * Seleciona o drone que melhor aproveita sua capacidade ao receber o pedido (o mais cheio possível sem exceder o limite).
   * Caso um drone seja encontrado, o pedido é atribuído.
4. Retorna todos os drones com suas entregas atribuídas.

Esse processo garante que pedidos prioritários sejam alocados primeiro e que cada drone carregue o máximo possível sem ultrapassar sua capacidade, reduzindo desperdício de carga.

---

## Banco de Dados

A aplicação utiliza MySQL com a seguinte estrutura:

CREATE DATABASE EntregasDrone;
USE EntregasDrone;

CREATE TABLE Drones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    DistanciaMax DOUBLE NOT NULL,
    CargaMax DOUBLE NOT NULL
);

CREATE TABLE Pedidos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Peso DOUBLE NOT NULL,
    ClienteX INT NOT NULL,
    ClienteY INT NOT NULL,
    Prioridade INT NOT NULL,
    DroneId INT,
    FOREIGN KEY (DroneId) REFERENCES Drones(Id)
);

## Configuração da conexão

No arquivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EntregasDrone;Uid=SEU_USUARIO;Pwd=SUA_SENHA"
}

Substitua SEU_USUARIO e SUA_SENHA pelas credenciais do banco de dados local.

---

## Como Executar

1. Clonar o repositório:

git clone https://github.com/seuusuario/simulador-drones.git
cd simulador-drones

2. Restaurar pacotes:
dotnet restore
3. Configurar banco no appsettings.jso
4. Rodar a aplicação:
dotnet run
5. A API estará disponível em:
https://localhost:7210

---

## Endpoints Principais

* **POST /Pedido** → Criar pedido
* **GET /Pedido** → Listar pedidos
* **GET /Pedido/{id}** → Buscar pedido por id
* **POST /Drone** → Registrar drone
* **GET /Drone** → Listar drones
* **GET /Pedido/DronesAtribuidos** → Distribuir pedidos e retornar drones com entregas

O endpoint [HttpGet("DronesAtribuidos")] pode ser entendido como um relatório resumido da operação do sistema: ele executa a lógica de distribuição de pedidos e retorna todos os drones com os pedidos atribuídos, permitindo visualizar de forma consolidada como os recursos foram utilizados.

---

## Exemplos de Testes no Postman

Durante os testes, foram utilizados os seguintes endpoints e exemplos de JSON:

* **GET listar drones**
https://localhost:7210/Drone

* **POST cadastrar drone**
https://localhost:7210/Drone

{
  "nome": "Mavic 3.0",
  "distanciaMax": 6.0,
  "cargaMax": 20.0
}

* **POST criar pedido**
https://localhost:7210/Pedido

{
  "Peso": 0.5,
  "ClienteX": 1,
  "ClienteY": 0,
  "Prioridade": 1
}

* **GET listar pedidos**
https://localhost:7210/Pedido

* **GET relatório de drones com pedidos atribuídos**
https://localhost:7210/Pedido/DronesAtribuidos

---

## Observações e Pontos de Atenção

* Durante o desenvolvimento, a principal dificuldade esteve relacionada à modelagem da lógica de alocação e organização em camadas, buscando aplicar conceitos aprendidos até o momento na faculdade:
  1. Programação Orientada a Objetos (princípio da responsabilidade única, separação em classes e serviços).
  2. Uso de exceções para tratamento de erros.
  3. LINQ para ordenações e consultas de forma mais expressiva.
  4. Experiência interdisciplinar adquirida na matéria Trabalho de Aplicações para Processos de Negócios, que contribuiu para pensar no tipo de implementação que eu poderia utilizar no case.

* Em relação aos testes unitários: embora tenha pesquisado e estudado a estrutura de testes com xUnit, ainda não consegui implementá-los neste projeto, principalmente por limitação de tempo e porque a disciplina de testes não foi abordada formalmente na faculdade (atualmente estou no 4º período).

* Para compensar, utilizei IA (ChatGPT) como apoio para montar cenários de teste no Postman (Como citei acima), validar os resultados da API e avaliar a qualidade da distribuição de pedidos.

* A escolha do Dapper para acesso ao banco de dados se deu por ser uma alternativa leve, performática e de fácil integração, além de permitir maior controle das queries SQL.

### Pontos de atenção futuros incluem:

  * Implementar testes unitários completos para validar cenários de carga.
  * Expandir as regras de negócio para contemplar bateria dos drones, otimização por proximidade e relatórios mais detalhados.

---

## Contato

A elaboração desse case me impactou para além da oportunidade de participar do processo seletivo de uma empresa incrível como a DTI, mas como um momento de aprofundamento em conceitos e lógicas vistas durante meu processo de aprendizado!

* Autor: Sther Marinho Brito
* LinkedIn: https://www.linkedin.com/in/sther-marinho

