<h1 align="center">
     <a href="#"> IoT House Automation Project </a>
</h1>

<h3 align="center">
    The main objective of this project is to bring together all the knowledge obtained over the years and also new knowledge in a single multidisciplinary environment, using different programming languages, development techniques, frameworks, design patterns and architecture.
</h3>

---

## Project Structure

The application was developed with the intention to simulate a microservice environment with multiple IoT devices.

![Graph Architecture](https://i.imgur.com/vK06P9g.png)

- Each microservice was designed to be independent with their own database and own implementation to external services.
- Microservices can communicate between them in asynchronous and synchronous mode.
- Common resources were shared as a library and can be found in libraries folder.

---

## How it works

This project is divided into four parts:
1. Core environment (microservices folder)
2. Arduino (Todo)
3. Mobile (in development - mobile folder)
4. External Services



### Pre-requisites

Before you begin, you will need to have the following tools installed on your machine:
- [Git](https://git-scm.com).
- [Node.js](https://nodejs.org/en/).
- Configure [SQL Server]() on your machine or docker
- Configure [RabbitMQ]() on your machine or docker
- Configure [MongoDB]() on your machine, docker or cloud

---

## Tech Stack

The following languages and frameworks were used in the construction of the project:

#### **Auth**
-   **[.Net Core (C#)](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)**
-   **[.Net Standard (C#)](https://dotnet.microsoft.com/download)**
-   **[SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)**
-   **[Microsoft Identity Model Tokens](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens/6.5.0)**
-   **[Identity Model Tokens Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/6.5.0)**
-   **[Mapper](https://github.com/Katsshura/IoT-House-Automation/tree/master/Libraries/IoT.House.Automation.Libraries/IoT.House.Automation.Libraries.Mapper)**
-   **[Config Loader](https://github.com/Katsshura/IoT-House-Automation/tree/master/Libraries/IoT.House.Automation.Libraries/IoT.House.Automation.Libraries.ConfigLoader)**

#### **Mobile**
-   **[Java 14](https://www.oracle.com/java/technologies/javase/jdk14-archive-downloads.html)**
-   **[Spring Framework](https://spring.io/)**
-   **[Lombok](https://projectlombok.org/)**
-   **[RabbitMQ](https://www.rabbitmq.com/)**

#### **Arduino**
-   **[.Net Core (C#)](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)**
-   **[.Net Standard (C#)](https://dotnet.microsoft.com/download)**
-   **[RabbitMQ](https://www.rabbitmq.com/)**
-   **[MongoDB](https://www.mongodb.com/)**
-   **[SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)**
-   **[Quartz](https://www.nuget.org/packages/Quartz/3.0.7)**
-   **[Mapper](https://github.com/Katsshura/IoT-House-Automation/tree/master/Libraries/IoT.House.Automation.Libraries/IoT.House.Automation.Libraries.Mapper)**
-   **[Config Loader](https://github.com/Katsshura/IoT-House-Automation/tree/master/Libraries/IoT.House.Automation.Libraries/IoT.House.Automation.Libraries.ConfigLoader)**

#### **Notification**
-   **[amqplib](https://www.npmjs.com/package/amqplib)**
-   **[dotenv](https://www.npmjs.com/package/dotenv)**
-   **[firebase-admin](https://www.npmjs.com/package/firebase-admin)**
-   **[inversify](http://inversify.io/)**
-   **[uuid](https://www.npmjs.com/package/uuid)**
-   **[rxjs](https://www.npmjs.com/package/rxjs)**

#### **Logger** (Todo)
- **[GoLang](https://golang.org/)**

---

## License

This project is under the license [GNU General Public License v3.0](./LICENSE).

---

## Get in touch
[![Linkedin Badge](https://img.shields.io/badge/-LinkedIn-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/katsshura/)](https://www.linkedin.com/in/katsshura/)
[![Gmail Badge](https://img.shields.io/badge/-Gmail-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:xr.emerson@gmail.com)](mailto:xr.emerson@gmail.com)
