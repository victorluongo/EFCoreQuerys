# EFCore Querys
Reference guide for EFCore Query commands and methods in Microsoft EntityFramework Core.


## Requirements

**.NET Core Framework**

https://dotnet.microsoft.com/download

**Visual Studio Code**

https://code.visualstudio.com/


## VS Code Extensions

**C# for Visual Studio Code (powered by OmniSharp)**

https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

**C# Extensions**

https://marketplace.visualstudio.com/items?itemName=kreativ-software.csharpextensions

**C# Snippets**

https://marketplace.visualstudio.com/items?itemName=jorgeserrano.vscode-csharp-snippets

**C# Full namespace autocompletion**

https://marketplace.visualstudio.com/items?itemName=adrianwilczynski.namespace

**SQLite Database Viewer**

https://marketplace.visualstudio.com/items?itemName=qwtel.sqlite-viewer


## Documentation

**Microsoft EntityFramework Core**

https://docs.microsoft.com/en-us/ef/core/

**Migrations**

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli


## Project Creation Scripts

**Create new solution**

    dotnet new sln -n EFCoreQuerys

**Create new console project**

    dotnet new console -n EFCoreQuerys -o EFCoreQuerys -f netcoreapp3.1

**Add project to solution**

    dotnet sln EFCoreQuerys.sln add EFCoreQuerys/EFCoreQuerys.csproj


## Project packages

**EntityFrameworkCore**

    dotnet add package Microsoft.EntityFrameworkCore --version 5.0.10

**EntityFrameworkCore Design**

    dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.10

**SQLite**

    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 5.0.3
