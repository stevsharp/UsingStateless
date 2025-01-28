# State Machine Workflow with EF Core and Stateless

This project demonstrates a console application that uses:

- **Entity Framework Core** for database interactions (with SQLite).
- **Stateless** for implementing a state machine workflow.
- **Dependency Injection** for managing services.

The application simulates an order processing system, where an order must go through predefined steps before completion. It includes error handling for invalid transitions and ensures steps are processed in order.

---

## Features

- **State Machine Workflow**: Uses the Stateless library to enforce state transitions.
- **Step Management**: Each order has multiple steps that must be completed sequentially.
- **Dependency Injection**: Follows modern .NET best practices.
- **SQLite Database**: Stores orders, steps, and state transitions.
- **Scenarios**:
  - **Happy Path**: All steps completed in order.
  - **Error Path**: Attempts to skip steps trigger validation errors.

---

## Prerequisites

- [.NET 6 or later](https://dotnet.microsoft.com/download)
- SQLite (optional, for database inspection)

---

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/state-machine-workflow.git
   cd state-machine-workflow
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Apply migrations to create the SQLite database:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

---

## Project Structure

- `App.cs`: Entry point for the console application, handles scenarios.
- `OrderService.cs`: Manages order and step processing.
- `ApplicationDbContext.cs`: Configures EF Core with SQLite.
- `Models`:
  - `Order`, `Step`, and `Comment`: Represent the data structure.
  - `OrderState` and `OrderTrigger`: Enum definitions for states and triggers.
- `Program.cs`: Configures the application host and dependency injection.

---

## Usage

### Happy Scenario: Completing Steps in Order

1. Creates a new order with predefined steps:
   - `Make Deposit`
   - `Review Documents`
   - `Approve Order`

2. Completes the steps in the correct order.

#### Output Example:

```
=== Happy Scenario: Completing Steps in Order ===
Order 1 created successfully!
Processing step: Make Deposit
Order is now in state: ReviewingDocuments
Processing step: Review Documents
Order is now in state: ApprovingOrder
Processing step: Approve Order
Order is now in state: Completed
Order 1 is now in state: Completed
```

### Error Scenario: Skipping Steps

1. Attempts to skip `Make Deposit` and go directly to `Review Documents`.
2. Displays an error and processes the steps in the correct order.

#### Output Example:

```
=== Bad Scenario: Trying to Complete Steps Out of Order ===
Order 2 created successfully!
Step 'Review Documents' cannot be completed because previous steps are not completed.
Processing step: Make Deposit
Order is now in state: ReviewingDocuments
Processing step: Review Documents
Order is now in state: ApprovingOrder
Processing step: Approve Order
Order is now in state: Completed
Order 2 is now in state: Completed
```

---

## How It Works

1. **State Machine**:
   - Configures transitions between states (`PendingDeposit`, `ReviewingDocuments`, `ApprovingOrder`, `Completed`).
   - Validates transitions using triggers (`MakeDeposit`, `ReviewDocuments`, `ApproveOrder`).

2. **Step Validation**:
   - Ensures steps are completed sequentially.
   - Skipping steps triggers validation errors.

3. **Database Integration**:
   - Uses EF Core to persist orders and steps.
   - SQLite as the database provider.

---

## Technologies Used

- [.NET 6](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Stateless](https://github.com/dotnet-state-machine/stateless)
- SQLite

---

## Troubleshooting

### Migration Issues

- **Error**: "No such table: Orders"
  - Ensure migrations are applied:
    ```bash
    dotnet ef database update
    ```
  - Delete the `app.db` file and reapply migrations if necessary.

### Dependency Issues

- Ensure required NuGet packages are installed:
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  dotnet add package Stateless
  ```

---

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

