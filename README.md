# Movie Store

This repository is a one stop trip to learn everything about building web application using .NET technologies and modern frontend framework Angular. The project is a movie store web application which offers hundreds of movies for purchase and also a platform for customers to share their reviews about the movies.

This repository contains two different architectures: web api services (ASP.NET Core Web api) plus single page application (Angular 8) and traditional MVC web app (ASP.NET Core MVC).

Note: This guide will focus on SPA and api services and more features are in progress.

## Environment and Architecture

### 🖥 Tech Stack

- _development environment_: .net core 3.1, Rider, Visual Studio Code
- _framework_: ASP.NET Web API, EF core, Angular 8, Nunit, moq, Boostrap
- _database_: Microsoft SQL server
- _tool_: Postman, Azure Data Studio
- 📅 [more content is in progress] _cloud services_: Azure SQL server, Azure App service, Azure Pipeline

### Architecutre

The backend is a typical **N-layer architecture**. **Dependency injection pattern** is used throughout the project. The frontend is a single page architecture organized using feature folders. Frontend communicates with backend using RESTful api.

## How to get started..

### Learn Full Stack Techniques

If you use this repository as a reference book to learn web developement using .NET technologies, I highly recommend you to follow all the issues (opened and closed) because it will show you how the whole development processes look like and what the challenges/requirements and what the actions I took. Personally, I would like to look at the commits that mentioned a specific issue to quickly remind myself how I solve the problem.

Hope the explanation of the file structure will help you find what you want:

```shell
.
├── LICENSE
├── MovieStore.API              # API layer, middleware/filters
├── MovieStore.Core             # abstraction (interface) of the code base, entity, model
├── MovieStore.Infrastructure   # implementation of abstraction
├── MovieStore.MVC              # controller and view of MVC pattern
├── MovieStore.UnitTests        # NUnit testing
├── MovieStore.sln
├── MovieStore.sln.DotSettings.user
├── MovieStoreSPA               # client side SPA
└── README.md
```

#### Tech Blogs

### Run locally

📅 [in progress [Issue #56](https://github.com/SiqiWang666/Movie-Store/issues/56)] Will containerize the application using Docker and publish to the Docker hub (the database is available in the Azure SQL server).

### Public Website (currently down)

📅 [in progress]

### Extend on it...

If you are interested in this project and want to build it by yourself, please feel free to reach out to me for the database scripts.
