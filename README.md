# PinewoodTechnical
My solution to the technical assessment set by Pinewood Tech

---

# PinewoodTech Customer Management System

## Overview

This is a full-stack Customer Management System built as part of a technical assessment for Pinewood Tech. The system allows users to perform basic CRUD (Create, Read, Update, Delete) operations on customers. It is composed of a backend developed with ASP.NET Core Web API and a frontend built with Razor Pages.

## Features

- View a list of customers
- Add a new customer
- Edit an existing customer’s details
- Delete a customer
- Frontend and backend are decoupled, communicating via API

Tech Stack:

### Backend
- **.NET Core 7.0** 
- **ASP.NET Core Web API** 
- **Entity Framework Core** for database interactions
- **AutoMapper** for mapping between data models and DTOs
- **PostgreSQL** for data storage

### Frontend
- **Razor Pages** with **C#**
- **JavaScript** for UI interactions (Customer Deletion)

### Libraries & Tools
- **Newtonsoft.Json** for JSON serialization/deserialization
- **HttpClient** for API calls
- **Bootstrap** for styling the UI
- **Jetbrains Rider** for development

## Setup Instructions:

### Prerequisites
- **.NET SDK** (version 7.0 or later)
- **Node.js** and **npm** (for Angular)
- **PostgreSQL** or another compatible database system

### Backend Setup

1. Clone the repository to your local machine:

2. Navigate to the backend project directory:

3. Set up your database connection in `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server_name;Database=CustomerDb;Trusted_Connection=True;"
    }
    ```

4. Run the migrations and update the database:
    dotnet ef database update

5. Run the backend API:
    dotnet run

### Frontend Setup

1. Navigate to the frontend directory:

2. Install dependencies:
    ```bash
    npm install
    ```
3. Start the frontend application:
    ```bash
    dotnet watch
    ```
    
### Running the Project

1. Make sure both the frontend and backend servers are running.
2. Open your browser and navigate to the frontend URL:
3. You should be able to see the customer management system and interact with the customers (view, add, edit, delete).

## API Endpoints

### GET /api/Customer
Returns a list of all customers.

### GET /api/Customer/{id}
Returns the details of a specific customer by their ID.

### POST /api/Customer
Adds a new customer. Requires a `CustomerDto` in the request body.

### PUT /api/Customer/{id}
Updates the details of a specific customer by their ID.

### DELETE /api/Customer/{id}
Deletes a specific customer by their ID.


## Acknowledgements

- PinewoodTech for providing the opportunity to complete this technical assessment.
