# Employee Management System

A web-based Employee Management System built with **ASP.NET MVC 5**, **ASP.NET Web API**, **Entity Framework 6**, **SQL Server**, and **jQuery/AJAX**.

The project was developed as a learning project to practice real-world application architecture, CRUD operations, authentication, authorization, REST APIs, and external authentication.

## Features

- User registration and login
- Employee management (Create, Read, Update, Delete)
- Department management
- Role-based authorization (Admin / Employee)
- JWT authentication for API security
- Google OAuth 2.0 login
- ASP.NET Web API endpoints
- Service Layer architecture
- Entity Framework 6 with Code First Migrations
- jQuery and AJAX-based form operations
- Partial Views for dynamic UI sections
- Client/server-side validation
- ELMAH error handling
- NLog configuration

## Technologies

- C#
- ASP.NET MVC 5
- ASP.NET Web API 2
- .NET Framework 4.8
- Entity Framework 6
- SQL Server
- HTML5 / CSS3
- Bootstrap
- JavaScript / jQuery / AJAX
- JWT
- Google OAuth 2.0
- Visual Studio 2022

## Architecture

The application follows a simple layered approach:

```text
Browser
   ↓
MVC Controllers / Views
   ↓
Web API Controllers
   ↓
Service Layer
   ↓
Entity Framework / DbContext
   ↓
SQL Server
```

Authentication and authorization are handled through JWT and Google OAuth, with role-based authorization for protected functionality.

## Project Structure

```text
MyLoginRegistration/
├── App_Start/       # MVC, Web API, routing and startup configuration
├── Common/          # Shared API response models
├── Controllers/     # MVC and Web API controllers
├── Database/        # Database-related SQL scripts
├── Migrations/      # Entity Framework Code First migrations
├── Models/          # Entity and view models
├── Security/        # JWT and authorization components
├── Services/        # Business/service layer
├── Views/           # Razor views and partial views
├── Content/         # CSS and frontend assets
├── Scripts/         # JavaScript/jQuery files
├── Web.config       # Local application configuration
└── Web.config.example # Configuration template
```

## Setup

### 1. Clone the repository

```bash
git clone <your-repository-url>
```

### 2. Restore NuGet packages

Open the solution in **Visual Studio 2022**. NuGet package restore should restore the packages listed in `packages.config`.

### 3. Configure application secrets

Open `Web.config` and replace:

```text
YOUR_GOOGLE_CLIENT_ID
YOUR_GOOGLE_CLIENT_SECRET
YOUR_LONG_RANDOM_JWT_SECRET
```

For Google login, configure the OAuth credentials in Google Cloud Console and add the appropriate redirect URI for your local application.

**Never commit real secrets to GitHub.**

### 4. Configure SQL Server

The project uses Entity Framework Code First. Configure your SQL Server/LocalDB environment as required by your development setup, then run the Entity Framework migrations from the Package Manager Console if necessary:

```powershell
Update-Database
```

### 5. Run

Open the solution in Visual Studio and run the application using IIS Express.

## Security Note

Secrets are intentionally replaced with placeholders in the public repository. Keep production credentials outside source control and use strong, randomly generated JWT signing keys.

## Learning Goals

This project was built to practice:

- MVC request/response flow
- Service Layer architecture
- REST API development
- AJAX and Partial Views
- Entity Framework Code First
- Authentication vs. authorization
- JWT-based API security
- Role-based access control
- OAuth 2.0 / Google authentication
- SQL Server database operations

## Future Improvements

- Add automated unit/integration tests
- Improve dependency injection
- Add centralized exception handling
- Move secrets to environment variables or a secure secret store
- Improve API documentation with Swagger/OpenAPI
- Add pagination and advanced employee search
