# Cosmos Odyssey

## Setup
1. Clone the project `git clone https://github.com/oskar-anderson/CosmosOdyssey`.
2. Insert your database connection string. 
Create file named `appsettings.secret.json` based on `appsettings.secret.template.json` structure (`cp webapp/appsettings.secret.template.json webapp/appsettings.secret.json`).
Insert your PostgreSQL database connection string.
3. `dotnet build`
4. `dotnet run --project WebApp`

## Description
### Architecture
Basically database `DbContext` interacts with `Domain` objects (`Domain` objects = database table = `DbSet`).
Repositories access Domain data through `DbSet` and map it to `DAL.DTO` objects before returning the result to web controllers.

Database delete behaviour is cascade delete.
Be careful with deleting parent properties.

Project layers:
* DAL - Layer 2 (knows how to query the database)
    * Contracts.DAL.App - Interfaces for repository methods
    * Contracts.DAL.Base - Base interfaces inherited by all `Contracts.DAL.App` interfaces.  This provides some basic functionality to all repositories assuming entities all have inherited ID property.
    * DAL.App.DTO - DTO objects returned to controllers
    * DAL.App.EF - Knows how to generate the database schema using `AppDbContext` class. Contains EF (Entity Framework ORM) database migration based on changes to `Domain` objects. Also contains concrete Repository implementations based on `Contracts.DAL.App` interfaces.
    * DAL.Base.EF - Implements `Contracts.DAL.Base`
* Domain - Layer 1 (innermost layer - database models)
    * Contracts.Domain - metadata interfaces for data objects
    * Domain.App - Contains the actual models database schema is based on. These objects can be inserted to the database.
    * Domain.Base - Implementations of `Contracts.Domain`
* Mapper - All manual data mapping between layers
* WebApp - The actual visible app. ASP.NET MVC frontend logic controllers.


## Packages
* Microsoft.EntityFrameworkCore - Base ORM package (DbContext)
* Npgsql.EntityFrameworkCore.PostgreSQL - EntityFramework Driver for Postgres database
* Microsoft.EntityFrameworkCore.Design - Needed for database schema generation with `dotnet ef migrations add ...` command otherwise error (Your startup project 'WebApp' doesn't reference `Microsoft.EntityFrameworkCore.Design`. This package is required for the Entity Framework Core Tools to work.)
* Microsoft.EntityFrameworkCore.Relational - EntityFrameworkCore extension package for relational databases. Needed for migrations (Microsoft.EntityFrameworkCore.Migrations (comes with Microsoft.EntityFrameworkCore package) relies on it).
* Microsoft.EntityFrameworkCore.Abstractions - precision attribute for EF decimal datatype
* Microsoft.Aspnetcore.Diagnostics.EntityFrameworkCore - builder.Services.AddDatabaseDeveloperPageExceptionFilter();
Identity:
  * Microsoft.Aspnetcore.Identity.UI - .AddDefaultIdentity()
  * Microsoft.Aspnetcore.Identity.EntityFrameworkCore - .AddEntityFrameworkStores()
* CRUD:
  * Microsoft.VisualStudio.Web.CodeGeneration.Design - Needed for CRUD generation
  * Microsoft.EntityFrameworkCore.SqlServer - Needed for CRUD generation

## Commands
Database related (run in solution folder)
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
~~~

Good to have generator for initial CRUD admin pages:
~~~
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator
~~~

Run in WebApp folder
```
dotnet aspnet-codegenerator controller -name CompanyController              -actions -m Company -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ParticipantBusinessController  -actions -m ParticipantBusiness -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ParticipantCivilianController  -actions -m ParticipantCivilian -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UISelectOptionController       -actions -m UISelectOption -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
```