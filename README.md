# PizzaHurt — Online Pizza Ordering

An ASP.NET Core MVC pizza-ordering application with separate core, model, repository, and service projects, plus dedicated user and administrator areas.

## Application areas and workflows

- Browse the catalog and item categories.
- Manage a shopping cart and place orders.
- Access order-related and account screens.
- Handle payment workflows through Razorpay integration.
- Provide administrative item, reporting, chart, and order-management screens.
- Record application activity and use structured logging.

The exact behavior of each integration depends on the configured backend services and valid development credentials.

## Technology stack

- C# and ASP.NET Core MVC on .NET 8
- Entity Framework Core and SQL Server
- Repository and service layers
- Razor views and Bootstrap
- Google authentication integration
- Razorpay payment integration
- Serilog structured logging

## Solution structure

~~~text
.
├── PizzaHurt.sln
├── PizzaHurt.UI/             # MVC UI, account/cart/payment controllers, Admin/User areas
├── PizzaHurt.Core/           # Entity classes and database context
├── PizzaHurt.Models/         # View and service models
├── PizzaHub.Services/        # Business services
└── PizzaHurt.Repositories/   # Data-access repositories
~~~

## Prerequisites

- .NET 8 SDK
- SQL Server and a database schema compatible with the entity model
- Google authentication credentials if that login flow is used
- Razorpay test credentials for payment-flow development

## Local setup

1. Clone the repository and open `PizzaHurt.sln` in Visual Studio 2022 or a compatible IDE.
2. Configure the required settings locally; see the next section.
3. Restore and build the solution:

~~~powershell
dotnet restore .\PizzaHurt.sln
dotnet build .\PizzaHurt.sln --configuration Release
~~~

4. Run the MVC application:

~~~powershell
dotnet run --project .\PizzaHurt.UI\PizzaHurt.UI.csproj
~~~

## Configuration and secrets

The committed `PizzaHurt.UI/appsettings.json` has been scrubbed of the previous connection string and authentication/payment values. Supply real values locally with .NET user secrets, environment variables, or an approved secret store.

Configuration keys include:

- `ConnectionStrings:DbConnection`
- `GoogleAuthSetting:ClientId`
- `GoogleAuthSetting:ClientSecret`
- `RazorPay:Key`
- `RazorPay:Secret`

Never commit SQL credentials, OAuth client secrets, or payment secrets. Use Razorpay test credentials during development and verify payment callbacks and server-side payment confirmation before production use.

## Production-readiness checklist

- Rotate any real credentials that were previously committed; cleaning the latest file does not erase old Git history.
- Review authorization rules for all Admin and User routes.
- Validate order totals and payment status on the server, not only in the browser.
- Verify error handling, logging redaction, and sensitive-data retention.
- Add automated tests for cart totals, order lifecycle, admin access, and payment failure paths.

## License

No license file is currently included. Add an explicit license before inviting others to reuse or redistribute the project.
