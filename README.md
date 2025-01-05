# Personal Finance Tracker API

### SECTION 1 - PROJECT OVERVIEW

## 📌 Overview

The **Personal Finance Tracker API** is a C# and .NET Core 8-based API designed to help users manage their finances efficiently. It provides endpoints for tracking expenses, budgeting, and logging transactions.

## 🚀 Features

- **Expense Tracking**: Log and categorize expenses via API endpoints.
- **Budgeting**: Set financial goals and budgets to manage spending.
- **Transaction Logging**: Record income and expenses with detailed tracking.

## 🏗️ Tech Stack

- **Backend**: C#, .NET Core 8
- **Database**: MS SQL Server
- **ORM**: Dapper
- **Authentication**: Identity Framework / JWT

## 📂 Project Structure
```
├── bin
├── Configurations
├── Controllers
│   ├── AccountControllers.cs
│   ├── AuthControllers.cs
│   ├── BudgetControllers.cs
│   └── TransactionControllers.cs
├── Data
│   └── DataContext.cs
├── DTOs
│   ├── UserLoginConfirmationDTO.cs
│   ├── UserLoginDTO.cs
│   └── UserRegistrationDTO.cs
├── Helpers
│   ├── AuthHelper.cs
│   └── SQLQueries.cs
├── Models
│   ├── AccountModel.cs
│   ├── BudgetsModel.cs
│   ├── TransactionModel.cs
│   └── UserModel.cs
├── obj
│   ├── Debug
│   ├── project.assets.json
│   └── project.nuget.cache
├── Properties
│   └── launchSettings.json
├── Repositories
│   ├── AccountRepository.cs
│   ├── BudgetRepository.cs
│   └── TransactionsRepository.cs
├── Services
│   ├── AccountServices.cs
│   ├── BudgetServices.cs
│   └── TransactionServices.cs
├── Tests
├── .gitignore
├── appsettings.Development.json
├── appsettings.json
├── PersonalFinanceTracker.csproj
├── PersonalFinanceTracker.http
├── PersonalFinanceTracker.sln
├── Program.cs
└── README.md
```

## 🛡️ Security Considerations

*   **Input Validation:** Prevents SQL injection and XSS attacks.
*   **Authentication & Authorization:** Implements JWT for secure access.
*   **Data Encryption:** Sensitive data is encrypted before storage.

## 📝 Future Enhancements

*   OAuth2 Support
*   AI-based Financial Insights
*   Multi-Currency Support

## 👨‍💻 Contributing

Contributions are welcome! Feel free to submit issues or pull requests.


### SECTION 2 - ENDPOINT DOCUMENTATION

### SECTION 3 - DEMO
