# Expense Tracker

Expense Tracker is a C# .NET console application to track your expenses.

## Getting Started

These instructions will guide you through setting up and running the project on your local machine.

### Prerequisites

- .NET Core SDK (version 7.0.302 or higher)
- SQLite database engine

### Installation

1. Clone the repository:
    ```shell
    git clone https://github.com/larryjing02/budget-app.git
    ```
2. Install the required dependencies. If you haven't installed the .NET Core SDK, please download and install it from the official .NET website.
3. Open a terminal or command prompt and navigate to the project directory.
4. Run the following command to create the initial migration:
    ```shell
    dotnet ef migrations add InitialCreate
    ```
    The above command generates a new EF Core migration based on the current state of your entities and creates a snapshot of the current database schema.
5. After creating the migration, apply it to the database by running the following command:
    ```shell
    dotnet ef database update
    ```
    The dotnet ef database update command applies any pending migrations to the database, updating the schema to match the latest migration.

### Usage
1. Start the application by running the following command:
    ```shell
    dotnet run
    ```