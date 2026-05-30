# AltaVisual - Sistema de Gestão para Gráficas Rápidas

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET Core](https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

O **AltaVisual** é uma aplicação web robusta desenvolvida para otimizar e gerenciar o fluxo de trabalho de gráficas rápidas. O sistema substitui o controle manual, garantindo a integridade dos dados desde o cadastro do cliente até a linha de produção e entrega final.

---

## Funcionalidades Principais

* **Gestão de Clientes:** Cadastro e controle de clientes da gráfica.
* **Sistema de Pedidos Integrado:** Criação de pedidos vinculados a clientes específicos.
* **Catálogo de Produtos Dinâmico:** Cadastro de Banners e Adesivos com regras de negócio exclusivas (ex: cálculo de área para adesivos, especificações de acabamento para lonas/faixas).
* **Workflow Visual de Status:** Acompanhamento do ciclo de vida do pedido através de marcadores visuais (*Em produção, Disponível para retirada, Pago, Retirado*).
* **Proteção de Dados (Integridade Referencial):** Bloqueio de exclusão acidental de clientes com pedidos em andamento e sistema de limpeza inteligente de vínculos (*Cascade/Clear*).

---

## 🛠️ Tecnologias e Arquitetura

Este projeto foi construído focando em performance, escalabilidade e boas práticas de Engenharia de Software:

* **Back-end:** C# com ASP.NET Core (Padrão de arquitetura **MVC - Model-View-Controller**).
* **Banco de Dados:** SQL Server.
* **ORM:** Entity Framework Core (Abordagem *Code-First*).
* **Front-end:** HTML5, CSS3, Razor Pages e **Bootstrap 5** para garantir uma interface moderna e 100% responsiva (Mobile-friendly).

### Destaques Técnicos

1. **Padrão TPH (Table-Per-Hierarchy):** O banco de dados foi modelado utilizando herança no C# (Classe base `Produto`, derivadas `Banner` e `Adesivo`). O EF Core unificou a hierarquia em uma única tabela utilizando a coluna `Discriminator`, garantindo altíssima performance nas consultas e eliminando a necessidade de `JOINs` complexos.
2. **Eager Loading vs Lazy Loading:** Utilização inteligente do método `.Include()` para evitar o problema de *N+1 queries*, otimizando o tempo de resposta ao carregar detalhes completos de pedidos com múltiplos itens na mesma transação.
3. **Data Annotations & ModelState:** Validação rigorosa de dados diretamente no back-end para evitar inconsistências matemáticas (ex: valores negativos) e campos vazios.

---

## Como executar o projeto localmente

### Pré-requisitos
* [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/) (com a carga de trabalho de desenvolvimento web e ASP.NET).
* SQL Server LocalDB (incluso no Visual Studio) ou SQL Server Management Studio.

### Passos para rodar
1. Faça o clone deste repositório:
   ```bash
   git clone [https://github.com/SEU-USUARIO/AltaVisual.git](https://github.com/SEU-USUARIO/AltaVisual.git)
2. Abra o arquivo AltaVisual.sln no Visual Studio.
3. Abra o Console do Gerenciador de Pacotes (Package Manager Console).
4. Restaure o banco de dados executando o comando:
   ```bash
   Update-Database

## 👨‍💻 Autores

Este projeto foi desenvolvido em equipe, unindo especialidades para entregar um sistema completo e robusto:

* **Fábio Suniga** - *Desenvolvimento Back-end e Arquitetura de Banco de Dados* - [LinkedIn]([https://www.linkedin.com/in/fabio-suniga/]) | [GitHub]([https://github.com/fabiosuniga])
* **Wesley Rosa** - *Desenvolvimento Front-end (UI/UX) e Quality Assurance (QA)* - [LinkedIn]([https://www.linkedin.com/in/wesleynunesrosa/]) | [GitHub]([https://github.com/wesleyrosa13])

### Algumas imagens do projeto:
<img width="1486" height="906" alt="image" src="https://github.com/user-attachments/assets/5911dfa6-9518-4799-8562-afe045c10efb" />
<img width="1520" height="856" alt="image" src="https://github.com/user-attachments/assets/31be7cb6-e90c-4d8e-9365-2dcb0d76487f" />
<img width="1382" height="905" alt="image" src="https://github.com/user-attachments/assets/1859c47e-6f7d-4ade-bbdb-445f0f256de6" />


