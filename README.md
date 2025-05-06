# 📚 Book Manager API – Quickfire API Creation

## 🔍 Overview
This project is a RESTful Book Manager API built using **ASP.NET Core Minimal APIs** with **Entity Framework Core** and a **SQL Server** backend. It demonstrates full CRUD functionality and follows a clean, professional structure.

## 👨‍🎓 Project Details
- **Module**: ADP631 FA2 – Advanced Design Pattern
- **Institution**: CTU Training Solutions
- **Assessment Type**: Formative Assessment (Individual)
- **NQF Level**: 6 | **Credits**: 20
- **Duration**: 2 Weeks

## 🛠 Technologies Used
- ASP.NET Core 8.0 (Minimal APIs)
- Entity Framework Core
- SQL Server (or LocalDB/SQLite)
- Swagger / Postman for testing
- Visual Studio / VS Code

## 📦 Features
- **GET /books**: Retrieve all books
- **GET /books/{id}**: Retrieve book by ID
- **POST /books**: Add a new book with validation
- **PUT /books/{id}**: Update book details with error handling
- **DELETE /books/{id}**: Remove a book by ID
- Proper use of HTTP status codes (`200`, `201`, `400`, `404`)
- Seeded initial data (at least 2 books)

## 🧱 Project Structure
All logic is implemented in a single `Program.cs` file using a Minimal API approach.

- `Book.cs` – Entity model
- `BookDbContext.cs` – Database context
- `appsettings.json` – Connection string config

## 🚀 Getting Started

### Prerequisites
- [.NET SDK 8](https://dotnet.microsoft.com/)
- SQL Server / LocalDB / SQLite
- Visual Studio or Visual Studio Code

### Setup
```bash
git clone https://github.com/yourusername/BookManagerAPI.git
cd BookManagerAPI
dotnet restore
dotnet ef database update
dotnet run
