# Taskivo Backend

Backend API for **Taskivo**, a task and reminder management application.

This repository contains the backend services, business logic, database integration, authentication, reminder processing, and APIs required by the Taskivo application.

## 🎯 Purpose

Taskivo Backend is being developed as a practical .NET backend project to learn and apply real-world backend engineering concepts, including:

* ASP.NET Core Web API
* CQRS
* Entity Framework Core
* Relational database design
* Authentication & Authorization
* Background processing
* Notification handling
* API design
* Testing
* Logging
* Deployment

## 🛠️ Technology Stack

### Backend

* **.NET / ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **CQRS**
* **MediatR**
* **FluentValidation**
* **JWT Authentication**
* **ASP.NET Core Background Services**
* **Swagger / OpenAPI**

### Database

**PostgreSQL** will be used as the primary relational database.

For development and deployment:

* Local PostgreSQL can be used for local development.
* **Neon PostgreSQL** will be used as the hosted database initially.

Neon provides managed PostgreSQL and currently offers a free plan suitable for development and learning projects.

## 📌 Planned Features

### Tasks

* Create, update and delete tasks
* Task status management
* Task priorities
* Due dates
* Categories and tags
* Search, filtering and sorting
* Pagination

### Reminders

* Create reminders for tasks
* One-time reminders
* Recurring reminders
* Background reminder processing
* Notification delivery
* Reminder status tracking

### Authentication

* User registration
* User login
* JWT authentication
* Refresh tokens
* Authorization

## 🏗️ Architecture

The backend will initially follow a **Modular Monolith architecture** with **CQRS**.

High-level flow:

```text
Client / Frontend
       │
       ▼
ASP.NET Core Web API
       │
       ▼
CQRS / Application
       │
       ▼
Domain
       │
       ▼
Infrastructure / EF Core
       │
       ▼
PostgreSQL
```

Reminder processing will be handled through background processing:

```text
PostgreSQL
    │
    ▼
Background Worker
    │
    ▼
Due Reminders
    │
    ▼
Notification Service
```

## 🌱 Branches

* `main` — stable code and completed milestones.
* `develop` — active development.

Feature development will take place on `develop` and will be merged into `main` when it reaches a stable state.

## 🚧 Current Status

**Initial Setup**

The repository currently contains the initial backend project setup.

Implementation and detailed documentation will be added progressively as development continues.

## 🗺️ Planned Development

1.  .NET solution and project setup
2.  Database and EF Core setup
3.  API foundation
4.  Authentication & authorization
5.  Task management
6.  Categories and tags
7.  Reminder management
8.  Background processing
9.  Notification integration
10. Testing
11. Logging and production improvements
12. Deployment

---

**Taskivo Backend** is being developed as a practical project to learn and apply real-world .NET backend engineering concepts.
