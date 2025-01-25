# Shary API Project
## Table of Contents
- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Endpoints](#endpoints)
- [Contributing](#contributing)
- [Support](#support)

## Overview
A scalable e-commerce platform built with modern architectural patterns and best practices. This platform leverages Entity Framework, LINQ, and follows Clean Architecture principles to provide a robust solution for e-commerce needs.

## Architecture
The project is structured into the following layers:

```
src/
├── Shary.APIs/       # API controllers and presentation layer
├── Shary.Core/       # Domain entities, interfaces, business logic
├── Shary.Repository/ # Data access implementation
├── Shary.Services/   # Business services implementation
```

### Project Structure Explanation
- **Shary.APIs**: Contains API controllers, filters, and configuration.
- **Shary.Core**: Houses domain entities, interfaces, and core business logic.
- **Shary.Repository**: Implements data access patterns and database operations.
- **Shary.Services**: Contains business service implementations and external integrations.

### Features
- **Onion Architecture**: Separation of concerns and maintainability.
- **Repository Pattern**: Abstraction of the data access layer and consistent interface for querying the database.
- **Unit of Work Pattern**: Management of the context and transaction of the Entity Framework.
- **Specification Pattern**: Building complex queries in a composable and maintainable way.
- **Stripe Payment Gateway**: Integration with Stripe for payment processing.

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- Redis

### Local Development
1. Update the connection strings in `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=SharyDb;User=sa;Password=YourStrong!Passw0rd;",
        "IdentityConnection": "Server=localhost;Database=SharyIdentity;User Id=sa;Password=YourStrong!Passw0rd;"
      },
      "Redis": {
        "ConnectionString": "localhost:6379"
      },
      "Jwt": {
        "SecretKey": "7f98e3b1-4b7b-4b7b-8b7b-7b7b7b7b7b7b",
        "ValidIssuer": "https://localhost:7284/",
        "ValidAudience": "Mohammed-Mostafa-Apis-Client",
        "TokenLifeTime": 60
      },
      "StripeSettings": {
        "PublishableKey": "",
        "SecretKey": ""
      }
    }
    ```
2. Run the application:
    ```sh
    dotnet restore
    dotnet run --project Shary.APIs
    ```

### API Documentation
- Postman documentation available [here](https://documenter.getpostman.com/view/35022618/2sAYQfEper)
- API documentation available at `https://localhost:7284/swagger/index.html`
- Detailed endpoint documentation in the Shary.APIs project

## Development Workflow
1. Make changes to the code.
2. Run the application:
    ```sh
    dotnet run --project Shary.APIs
    ```
3. Access the API at `http://localhost:8080`

## Endpoints

### AccountController
- `POST /api/account/login`: Login a user
- `POST /api/account/register`: Register a new user
- `GET /api/account`: Get the current user
- `GET /api/account/address`: Get the user's address
- `PUT /api/account/address`: Update the user's address
- `GET /api/account/emailexists`: Check if an email exists
- `PUT /api/account/update`: Update the user's information
- `PUT /api/account/updatepassword`: Update the user's password

### BasketController
- `GET /api/basket`: Get a basket by ID
- `POST /api/basket`: Update a basket
- `DELETE /api/basket/{id}`: Delete a basket by ID

### ErrorsController
- `GET /errors/{code}`: Handle errors

### OrdersController
- `POST /api/orders`: Create a new order
- `GET /api/orders`: Get orders for the current user
- `GET /api/orders/{id}`: Get an order by ID
- `GET /api/orders/deliveryMethods`: Get delivery methods

### ProductsController
- `GET /api/products`: Get a list of products
- `GET /api/products/{id}`: Get a product by ID
- `GET /api/products/brands`: Get a list of product brands
- `GET /api/products/categories`: Get a list of product categories

### PaymentsController
- `POST /api/payments`: Process a payment
- `GET /api/payments/{id}`: Get payment details by ID

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

## Support
For support, please create an issue in the repository.
