# Taskivo Backend

Taskivo Backend is a modular ASP.NET Core Web API for task management, user authentication, task ownership, status tracking, and priority management.

The project demonstrates layered architecture, CQRS-style command and query separation, Entity Framework Core, PostgreSQL integration, JWT authentication, centralized exception handling, and dependency injection.

## Features

### Authentication

- User registration and login
- Secure password hashing with `PasswordHasher<UserEntity>`
- JWT token generation and validation
- Username uniqueness validation
- Authenticated user identification through JWT claims

### Task Management

- Create, read, update, and delete tasks
- Retrieve only tasks owned by the authenticated user
- Update task status independently
- Due date and completion date support
- Priority support
- Soft deletion

### Master Data

Seeded task statuses:

- New
- In-Progress
- On Hold
- Completed
- Discarded

Seeded task priorities:

- Low
- Medium
- High

### API Platform Features

- RESTful API endpoints
- Swagger/OpenAPI documentation
- Bearer token support in Swagger
- PostgreSQL database support
- Entity Framework Core migrations
- Centralized exception handling
- Consistent API response format
- Model validation
- CORS configuration
- Cancellation token support for database operations

## Technology Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL
- Npgsql Entity Framework Core provider
- JWT Bearer Authentication
- Swagger/OpenAPI
- Nullable reference types
- Dependency Injection

## Solution Structure

```text
Taskivo Backend
|
|-- Taskivo-Controller
|   |-- Controllers
|   |-- Extensions
|   |-- Program.cs
|   |-- appsettings.json
|   `-- appsettings.Development.json
|
|-- Taskivo-AppServices
|   |-- AuthService.cs
|   |-- TaskService.cs
|   |-- IAuthService.cs
|   `-- ITaskService.cs
|
|-- Taskivo-Commands
|   |-- Command contracts
|   |-- Command handlers
|   `-- Auth and task commands
|
|-- Taskivo-Queries
|   |-- Query contracts
|   |-- Query handlers
|   `-- Auth and task queries
|
|-- Taskivo-DTO
|   |-- Authentication DTOs
|   `-- Task DTOs
|
|-- Taskivo-Infrastructure
|   |-- AppDbContext.cs
|   |-- Models
|   |-- Repositories
|   |-- Extensions
|   `-- Migrations
|
`-- Taskivo-Common
              |-- Exceptions
              |-- Extensions
              |-- Responses
              `-- Shared application components
```

## Application Architecture

Taskivo follows a layered modular monolith architecture.

```text
Client
       |
       v
ASP.NET Core Controllers
       |
       v
Application Services
       |
       |-- Commands and Command Handlers
       `-- Queries and Query Handlers
                                   |
                                   v
                     Repository Interfaces
                                   |
                                   v
                     Infrastructure Layer
                                   |
                                   v
                     Entity Framework Core
                                   |
                                   v
                     PostgreSQL
```

### Controller Layer

Controllers handle HTTP concerns, routing, authorization, authenticated user extraction, and HTTP responses. Database access and complex business rules remain outside the controller layer.

### Application Service Layer

`AuthService` coordinates registration, login, password hashing, password verification, and JWT generation.

`TaskService` validates task requests and delegates each operation to the appropriate command or query handler.

### Command Layer

Commands represent state-changing operations such as:

- `CreateUserCommand`
- `CreateTaskCommand`
- `UpdateTaskCommand`
- `UpdateTaskStatusCommand`
- `DeleteTaskCommand`

Each command is processed by a dedicated handler through the following contract:

```csharp
public interface ICommandHandler<TCommand, TResult>
{
              Task<TResult> Handle(
                            TCommand command,
                            CancellationToken cancellationToken = default);
}
```

### Query Layer

Queries represent read operations such as:

- `UserExistsQuery`
- `GetUserByUsernameQuery`
- `GetAllTaskQuery`
- `GetTaskByIdQuery`

Each query is processed by a dedicated query handler:

```csharp
public interface IQueryHandler<TQuery, TResult>
{
              Task<TResult> Handle(
                            TQuery query,
                            CancellationToken cancellationToken = default);
}
```

Separating reads from writes keeps individual use cases focused and reduces coupling between application operations.

### Infrastructure Layer

The infrastructure layer contains Entity Framework Core configuration, PostgreSQL access, repositories, entity models, relationships, migrations, and master data seed configuration.

## Database Design

The application uses PostgreSQL through Entity Framework Core.

### Main Entities

#### User

Stores the user ID, username, name, password hash, and audit timestamps. A unique database index prevents duplicate usernames.

#### Task

Stores the task title, description, status, priority, due date, completion date, audit timestamps, owner ID, and soft-delete flag.

#### Status and Priority

Both are master-data entities containing an ID, display name, color, and audit timestamps.

### Relationships

- A user can own multiple tasks.
- A task belongs to one user.
- A task references one status.
- A task references one priority.
- Related master data uses restricted delete behavior.

### Soft Delete

Task deletion sets `IsDeleted` to `true` instead of physically removing the row. Normal task queries exclude deleted records while preserving historical data.

## Authentication Flow

### Registration

1. The client submits username, name, and password.
2. The request is validated.
3. The username is checked for duplicates through a query handler.
4. The password is hashed.
5. A user creation command is processed.
6. The user is persisted through the repository.
7. A JWT token and user response are returned.

### Login

1. The client submits username and password.
2. The user is retrieved through a query handler.
3. The password is verified against the stored hash.
4. A JWT is generated for valid credentials.
5. The token and user information are returned.

JWT claims include the user ID, username, first name, and last name. Task endpoints use the authenticated user ID to enforce ownership.

## API Endpoints

The API uses the `/api` route prefix.

### Authentication

| Method | Endpoint | Description | Authentication |
|---|---|---|---|
| `POST` | `/api/Auth/signup` | Register a new user | Not required |
| `POST` | `/api/Auth/login` | Log in and receive a JWT | Not required |

### Tasks

| Method | Endpoint | Description | Authentication |
|---|---|---|---|
| `GET` | `/api/Task` | Get all tasks for the current user | Required |
| `GET` | `/api/Task/{id}` | Get one owned task | Required |
| `POST` | `/api/Task` | Create a task | Required |
| `PUT` | `/api/Task/{id}` | Update task details | Required |
| `PATCH` | `/api/Task/{id}/status` | Update task status | Required |
| `DELETE` | `/api/Task/{id}` | Soft-delete a task | Required |

Authenticated requests must include:

```http
Authorization: Bearer <jwt-token>
```

## API Response Format

Successful responses use the following structure:

```json
{
       "success": true,
       "message": "Task retrieved successfully.",
       "data": {}
}
```

Error responses use the following structure:

```json
{
       "success": false,
       "message": "Task not found.",
       "data": null
}
```

This provides clients with a predictable response contract across the API.

## Validation and Error Handling

Validation is applied through ASP.NET Core model validation, application service validation, business rules, and database constraints.

Examples include:

- Required authentication fields
- Required task titles
- Valid user IDs
- Valid task IDs
- Valid status IDs
- Duplicate usernames
- Missing JWT configuration

Business errors use `BusinessException`. The centralized exception handler logs unexpected exceptions, maps business exceptions to their HTTP status codes, and returns a consistent JSON error response. Unexpected exceptions return a generic `500 Internal Server Error` message.

## Dependency Injection

The application registers the following through dependency injection:

- Database context
- Repositories
- Application services
- Command handlers
- Query handlers
- Exception handling services

Application layers depend on interfaces instead of concrete persistence implementations.

## Development Setup

### Prerequisites

- .NET 10 SDK
- PostgreSQL database
- Visual Studio Code or Visual Studio
- Entity Framework Core CLI tools when working with migrations

Verify the .NET installation:

```bash
dotnet --version
```

### Configure the Database

Set the PostgreSQL connection string in user secrets, environment variables, or the appropriate application settings file.

Example:

```json
{
       "ConnectionStrings": {
              "DefaultConnection": "Host=localhost;Database=taskivo;Username=postgres;Password=your-password"
       }
}
```

Database credentials must not be committed to source control.

### Configure JWT

```json
{
       "Jwt": {
              "Key": "replace-with-a-long-secret-key",
              "Issuer": "Taskivo",
              "Audience": "Taskivo-Client"
       }
}
```

Production JWT keys should be provided through a secure configuration provider or environment variable.

### Restore, Build, and Run

From the solution directory:

```bash
dotnet restore
dotnet build
dotnet run --project Taskivo-Controller
```

### Apply Database Migrations

```bash
dotnet ef database update \
       --project Taskivo-Infrastructure \
       --startup-project Taskivo-Controller
```

When running in Development, Swagger is available at `/swagger` on the generated application URL.

## Development Strategies

### Separation of Responsibilities

Each project has a focused responsibility. Controllers handle HTTP concerns, application services coordinate use cases, handlers process commands and queries, repositories handle persistence, and infrastructure configures the database.

### CQRS-Style Separation

Read and write operations are represented separately through commands and queries. This keeps individual use cases focused and allows read and write behavior to evolve independently.

### Repository Abstraction

Application logic does not directly depend on Entity Framework Core queries. Repository interfaces isolate persistence logic and support easier testing and future implementation changes.

### User Ownership Enforcement

Task commands and queries receive the authenticated user ID. Repository operations filter by both task ID and user ID, preventing users from accessing or modifying another user's tasks.

### Centralized Response Handling

Shared response classes provide a consistent API contract for successful and failed operations.

### Cancellation Support

Database and handler operations accept `CancellationToken` values so work can be cancelled when a request ends early.

### Master Data Seeding

Statuses and priorities are seeded through Entity Framework Core model configuration so required reference data is available consistently.

### Incremental Feature Development

The implementation has progressed through the following milestones:

1. Solution and project setup
2. Database and Entity Framework Core configuration
3. Task CRUD operations
4. Task statuses and priorities
5. Authentication and authorization
6. Task ownership enforcement
7. CQRS-based authentication handlers
8. Centralized exception handling and API responses

## Current Implementation Status

### Implemented

- Modular project structure
- ASP.NET Core Web API
- PostgreSQL integration
- Entity Framework Core migrations
- Task CRUD operations
- Task ownership
- Soft deletion
- Status and priority master data
- User registration and login
- Password hashing
- JWT authentication and authorization
- Swagger/OpenAPI
- Centralized exception handling
- Consistent API responses
- CQRS-style command and query handlers

### Future Enhancements

The following features are planned but are not currently implemented:

- Refresh tokens
- Password reset
- Role-based authorization
- Categories and tags
- Search, filtering, and sorting
- Pagination
- Task reminders
- Recurring reminders
- Background reminder processing
- Notification delivery
- Automated unit and integration tests
- Structured production logging
- Health checks
- Deployment automation
- Production secret management

## Security Considerations

- Passwords are stored as hashes, not plain text.
- Task access is restricted to the authenticated owner.
- JWT issuer, audience, lifetime, and signing key are validated.
- Production JWT keys must not be committed to source control.
- Database credentials must be stored securely.
- CORS should be restricted to trusted frontend origins before production deployment.
- HTTPS should be enabled in non-local environments.

## Branching Strategy

- `develop` is used for active development.
- `main` is intended for stable releases and completed milestones.
- Feature work should be developed incrementally and merged after validation.

## License

This project is currently being developed as a practical learning and assessment project.
