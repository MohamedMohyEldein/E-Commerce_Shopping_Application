# ShopEase - ASP.NET Core Web API

A professional, scalable e-commerce backend API built with ASP.NET Core, demonstrating modern software development practices and clean architecture principles.

[![.NET](https://img.shields.io/badge/.NET-Core-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-Latest-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/EF%20Core-ORM-512BD4)](https://docs.microsoft.com/en-us/ef/core/)

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Design Patterns](#design-patterns)
- [Technology Stack](#technology-stack)
- [Features](#features)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Security](#security)
- [Database](#database)
- [Installation & Setup](#installation--setup)
- [Key Highlights](#key-highlights)

---

## Overview

ShopEase is a well-structured ASP.NET Core Web API for a shopping application that follows modern software development practices. The project demonstrates a strong understanding of building scalable and maintainable backend systems using Clean Architecture, CQRS pattern, and industry-standard design patterns.

The API provides comprehensive functionality for:
- **User Authentication & Authorization**
- **Product & Category Management**
- **Shopping Cart Operations**
- **Order Processing**
- **Wishlist Management**

---

## Architecture

### Clean Architecture

The project is organized into **four distinct layers** to promote separation of concerns, testability, and maintainability:

#### 1. **ShoppingApp.Domain**
- Contains core business models (entities)
- Defines business rules and domain logic
- Independent of external concerns

#### 2. **ShoppingApp.Application**
- Orchestrates business logic
- Independent of UI or data access concerns
- Implements CQRS pattern with Commands and Queries
- Handles validation and business workflows

#### 3. **ShoppingApp.Infrastructure**
- Handles external concerns
- Database access via Entity Framework Core
- File system operations
- Third-party service integrations

#### 4. **ShoppingApp.Presentation**
- Exposes application functionality via RESTful API
- Handles HTTP requests and responses
- API endpoint definitions

---

## Design Patterns

### CQRS (Command Query Responsibility Segregation)

The application layer uses the **CQRS pattern** to separate read and write operations:
- **Commands**: Handle state changes and data modifications
- **Queries**: Handle data retrieval operations
- Implemented in the `Features` directory with distinct command and query handlers

### Mediator Pattern

The **MediatR library** decouples the request/response pipeline:
- Controllers send commands and queries to MediatR
- MediatR dispatches requests to appropriate handlers
- Reduces direct dependencies between controllers and business logic
- Enables cross-cutting concerns via pipeline behaviors

### Repository and Unit of Work

The infrastructure layer implements data access patterns:
- **GenericRepository<T>**: Provides common CRUD operations
- **Unit of Work**: Coordinates transactions across multiple repositories
- Ensures data consistency and integrity
- Reduces code duplication

### Dependency Injection

Extensive use of dependency injection for:
- Service lifecycle management
- Loose coupling between components
- Testability and flexibility
- Registered in: `ApiDependencies.cs`, `ApplicationDependencies.cs`, `InfrastructureDependencies.cs`

---

## Technology Stack

### Core Framework
- **ASP.NET Core** - Modern web framework
- **Entity Framework Core** - Object-Relational Mapper (ORM)
- **C#** - Primary programming language

### Libraries & Packages
- **MediatR** - Mediator pattern implementation
- **FluentValidation** - Request validation
- **Microsoft.AspNetCore.Identity** - User management and authentication
- **JWT Bearer Authentication** - Token-based security

### Database
- **Entity Framework Core** - Database operations
- **SQL Server** (or configurable database provider)

---

## Features

### Core Functionality

✅ **User Authentication & Authorization**
- User registration with email validation
- Secure login with JWT token generation
- Role-based access control
- Password hashing and security policies

✅ **Product Management**
- CRUD operations for products
- Product search and filtering
- Category-based organization
- Product details and pricing

✅ **Category Management**
- Hierarchical category structure
- CRUD operations for categories
- Product-category associations

✅ **Shopping Cart**
- Add/remove items from cart
- Update quantities
- Cart persistence per user
- Price calculations

✅ **Order Processing**
- Create orders from cart
- Order history tracking
- Order status management

✅ **Wishlist**
- Save favorite products
- Manage wishlist items
- Move items to cart

---

## Project Structure

```
ShopEase/
│
├── ShoppingApp.Domain/
│   ├── Entities/
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── User.cs
│   │   ├── Order.cs
│   │   └── Cart.cs
│   └── Business Rules
│
├── ShoppingApp.Application/
│   ├── Features/
│   │   ├── Products/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateProduct/
│   │   │   │   ├── UpdateProduct/
│   │   │   │   └── DeleteProduct/
│   │   │   └── Queries/
│   │   │       ├── GetProducts/
│   │   │       └── GetProductById/
│   │   ├── Categories/
│   │   ├── Cart/
│   │   ├── Orders/
│   │   └── Wishlist/
│   ├── Behaviors/
│   │   └── ValidationBehavior.cs
│   ├── Validators/
│   │   └── CreateProductDtoValidator.cs
│   └── ApplicationDependencies.cs
│
├── ShoppingApp.Infrastructure/
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── Configurations/
│   ├── Repositories/
│   │   ├── GenericRepository.cs
│   │   └── UnitOfWork.cs
│   ├── Services/
│   │   └── JwtTokenService.cs
│   └── InfrastructureDependencies.cs
│
└── ShoppingApp.Presentation/
    ├── Controllers/
    │   ├── AuthController.cs
    │   ├── ProductsController.cs
    │   ├── CategoriesController.cs
    │   ├── CartController.cs
    │   ├── OrdersController.cs
    │   └── WishlistController.cs
    ├── Middleware/
    │   └── ExceptionHandlingMiddleware.cs
    └── ApiDependencies.cs
```

---

## API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/login` | Login and receive JWT token |

### Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Products` | Get all products |
| GET | `/api/Products/{id}` | Get product by ID |
| POST | `/api/Products` | Create new product |
| PUT | `/api/Products/{id}` | Update product |
| DELETE | `/api/Products/{id}` | Delete product |

### Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Categories` | Get all categories |
| GET | `/api/Categories/{id}` | Get category by ID |
| POST | `/api/Categories` | Create new category |
| PUT | `/api/Categories/{id}` | Update category |
| DELETE | `/api/Categories/{id}` | Delete category |

### Cart
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Cart` | Get user's cart |
| POST | `/api/Cart` | Add item to cart |
| PUT | `/api/Cart` | Update cart item |
| DELETE | `/api/Cart/{id}` | Remove item from cart |

### Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Orders` | Get user's orders |
| GET | `/api/Orders/{id}` | Get order by ID |
| POST | `/api/Orders` | Create new order |

### Wishlist
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Wishlist` | Get user's wishlist |
| POST | `/api/Wishlist` | Add item to wishlist |
| DELETE | `/api/Wishlist/{id}` | Remove item from wishlist |

---

## Security

### JWT Authentication

**JSON Web Tokens (JWT)** secure API endpoints:
- `JwtTokenService` generates secure tokens
- `AddJwtBearer` configuration validates incoming tokens
- Token-based stateless authentication

### Identity Framework

**Microsoft.AspNetCore.Identity** provides:
- Secure password hashing
- User management
- Role-based authorization
- Account lockout policies

### Password Policies

Strong password requirements enforced through Identity options:
- Minimum length requirements
- Complexity rules (uppercase, lowercase, digits, special characters)
- Password history tracking
- Configured in `InfrastructureDependencies.cs`

---

## Database

### Entity Framework Core

**ORM Features**:
- Code-first migrations
- LINQ-based queries
- Change tracking
- Relationship management

### Generic Repository

`GenericRepository<T>` centralizes common database operations:
- Create, Read, Update, Delete (CRUD)
- Reduces code duplication
- Consistent data access patterns

### Optimization Techniques

✅ **Asynchronous Operations**
- All database operations use `async/await`
- Improves scalability by not blocking threads
- Better resource utilization

✅ **AsNoTracking()**
- Used for read-only queries
- Improves performance by disabling change tracking
- Reduces memory overhead for query operations

---

## Validation

### FluentValidation

Request validation using **FluentValidation** library:
- Validators defined for DTOs (e.g., `CreateProductDtoValidator`)
- Enforces business rules and data integrity
- Clear, expressive validation syntax

### Validation Behavior

`ValidationBehavior` registered in MediatR pipeline:
- Automatically validates incoming commands and queries
- Executes before handler processing
- Ensures handlers only receive valid data
- Returns validation errors to client

---

## Exception Handling

### Custom Exception Middleware

`ExceptionHandlingMiddleware` provides centralized exception handling:
- Catches exceptions from anywhere in the application
- Translates exceptions to appropriate HTTP status codes
- Returns consistent JSON error responses
- Logs exceptions for debugging

### Custom Exception Types

Specific exception types for different scenarios:
- `NotFoundException` - Resource not found (404)
- `BadRequestException` - Invalid request (400)
- Handled uniformly by middleware

---

## Installation & Setup

### Prerequisites
- .NET 6.0 or higher
- SQL Server (or preferred database)
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/shopease.git
   cd shopease
   ```

2. **Configure the database connection**
   
   Update `appsettings.json` in ShoppingApp.Presentation:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=ShopEase;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd ShoppingApp.Presentation
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the API**
   
   Navigate to: `https://localhost:5001/swagger`

---

## Key Highlights

### Professional Development Practices

✅ **Clean Architecture** - Separation of concerns across four distinct layers

✅ **CQRS Pattern** - Separate read and write operations for scalability

✅ **Mediator Pattern** - Decoupled request/response pipeline

✅ **Repository & Unit of Work** - Consistent data access and transaction management

✅ **Dependency Injection** - Loose coupling and testability

✅ **Comprehensive Validation** - FluentValidation with pipeline behaviors

✅ **Centralized Exception Handling** - Consistent error responses

✅ **JWT Authentication** - Secure token-based authentication

✅ **Asynchronous Operations** - Scalable I/O operations

✅ **Code Reusability** - Generic repository pattern

---

## Author

**Mohamed Mohyeldein Amr Ahmed Hassan**

This project demonstrates proficiency in:
- Modern .NET development
- Clean Architecture principles
- RESTful API design
- Security best practices
- Database optimization
- Design pattern implementation

---

## License

This project is available for educational and portfolio purposes.

---

**Built with 💙 using ASP.NET Core**
