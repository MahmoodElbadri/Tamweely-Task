# 📘 Tamweely Address Book
# The code of frontend is here ![FrontEnd](https://github.com/MahmoodElbadri/Tamweely.Client/).
A full-stack modern Address Book application built to demonstrate **Clean Architecture**, **Secure Authentication**, and **Advanced Data Handling**.

![Project Status](https://img.shields.io/badge/Status-Completed-success)
![.NET](https://img.shields.io/badge/.NET-8.0-512bd4)
![Angular](https://img.shields.io/badge/Angular-17%2B-dd0031)
![License](https://img.shields.io/badge/License-MIT-blue)

## 🌟 Features

* **🔐 Authentication & Security:**
    * Full Login/Register system using **ASP.NET Core Identity**.
    * Secured with **JWT (JSON Web Tokens)**.
    * Angular **Functional Interceptors** & **Guards** for route protection.
* **📇 Contact Management:**
    * Create, Read, Update, and Delete (CRUD) contacts.
    * **Image Upload** handling (stored locally/server-side).
    * Rich data entry (Date of Birth, Job Titles, Departments).
* **🔍 Advanced Search & Export:**
    * Filter by Name, Mobile, Email, or Address.
    * Filter by Date of Birth range.
    * **Export to Excel** feature using ClosedXML.
* **📐 Modern Architecture:**
    * **Backend:** Onion Architecture (Domain, Application, Infrastructure, API).
    * **Frontend:** Angular Modular Design with Lazy Loading.

---

## 🏗️ Architecture & Tech Stack

### 🔙 Backend (.NET Core Web API)
The backend is built using **Onion Architecture** to ensure separation of concerns and testability.

* **Framework:** .NET 9 Web API.
* **Database:** SQL Server with Entity Framework Core (Code First).
* **Patterns:** Repository Pattern, Generic Repository, Unit of Work approach.
* **Mapping:** AutoMapper.
* **Libraries:**
    * `Microsoft.AspNetCore.Identity` (Auth).
    * `ClosedXML` (Excel Export).
    * `System.IdentityModel.Tokens.Jwt`.

### 🅰️ Frontend (Angular)
Built with the latest Angular features focusing on performance and code cleanliness.

* **Framework:** Angular 17+.
* **Core Concepts:**
    * **Standalone Components**.
    * **Signals** for state management.
    * **Functional Interceptors & Guards**.
    * **Lazy Loading** for Modules (Auth, AddressBook).
* **Styling:** Bootstrap 5 & SCSS.
* **Libraries:** `ngx-toastr` (Notifications), `ngx-bootstrap`.

---

## 🚀 Getting Started

Follow these instructions to get the project up and running on your local machine.

### Prerequisites
* [Node.js](https://nodejs.org/) (Latest LTS).
* [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later).
* SQL Server.

### 1️⃣ Backend Setup
1.  Clone the repository.
2.  Navigate to `Tamweely.Api` folder.
3.  Update the **Connection String** in `appsettings.json` to match your local SQL Server instance.
4.  Run Migrations to create the database:
    ```bash
    dotnet ef database update --project ../Tamweely.Infrastructure --startup-project .
    ```
5.  Run the API:
    ```bash
    dotnet run
    ```
    *The API will start at `https://localhost:7xxx` (Check launchSettings.json).*

### 2️⃣ Frontend Setup
1.  Navigate to `TamweelyClient` folder.
2.  Install dependencies:
    ```bash
    npm install
    ```
3.  Open `src/environments/environment.ts` and ensure `apiUrl` matches your running Backend port.
4.  Run the application:
    ```bash
    ng serve -o
    ```
5.  Navigate to `http://localhost:4200`.

---


## 🔗 API Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/Auth/login` | Authenticate user & get Token |
| `GET` | `/api/AddressBook` | Get all contacts |
| `POST` | `/api/AddressBook` | Create new contact (FormData) |
| `GET` | `/api/AddressBook/search` | Search with filters |
| `GET` | `/api/AddressBook/export` | Download Excel file |

---

## 👤 Author

**Mahmood Elbadri**
* Full-Stack Developer (.NET & Angular)
* [GitHub Profile](https://github.com/MahmoodElbadri)

---
