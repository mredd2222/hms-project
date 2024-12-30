
# Healthcare Management System API

A robust backend solution for a healthcare management system designed to manage patient records, appointments, lab results, and billing. This project is built with **ASP.NET Core** and **Entity Framework Core** and supports functionalities like user roles, digital signature collection, appointment management, and more.

## Table of Contents

-   [Features](#features)
-   [Technologies](#technologies)
-   [Setup and Installation](#setup-and-installation)
-   [API Endpoints](#api-endpoints)
-   [Database Schema](#database-schema)
-   [Contributing](#contributing)
-   [License](#license)

## Features

-   **User Roles**: Differentiates between `Patient` and `Staff` roles.
-   **Appointment Management**: Manages appointments, allowing links between patients and providers.
-   **Digital Signatures**: Collects and verifies digital signatures securely.
-   **Patient Dashboard**: Aggregates patient data such as upcoming appointments, stats, and recent lab results.
-   **Secure File Uploads**: Uploads and stores PDF files for patient forms and records.

## Technologies

-   **Backend**: ASP.NET Core
-   **Database**: SQL Server (using Entity Framework Core)
-   **Authentication & Authorization**: Role-based (Patient and Staff roles)
-   **Cryptography**: Digital signature collection and verification

## Setup and Installation

### Prerequisites

-   [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
-   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or configure a cloud-based SQL server)
-   Entity Framework CLI tools

### Getting Started

1.  **Clone the Repository**
    
    bash
    
    Copy code
    
    `git clone https://github.com/yourusername/healthcare-management-system-api.git
    cd healthcare-management-system-api` 
    
2.  **Install Dependencies**
    
    bash
    
    Copy code
    
    `dotnet restore` 
    
3.  **Set Up Database Configuration**
    
    -   Update the connection string in `appsettings.json` with your SQL Server details:
        
        json
        
        Copy code
        
        `"ConnectionStrings": {
          "MTCDatabase": "Data Source=your_server;Initial Catalog=your_database;User ID=your_user;Password=your_password"
        }` 
        
4.  **Run Migrations**
    
    bash
    
    Copy code
    
    `dotnet ef database update` 
    
5.  **Run the Application**
    
    bash
    
    Copy code
    
    `dotnet run` 
    
6.  **Access Swagger Documentation**
    
    -   Visit `https://localhost:5001/swagger` to explore the API endpoints and test them.

## API Endpoints

Endpoint

Method

Description

`/api/patient/{id}/dashboard`

GET

Fetch patient dashboard data

`/api/appointment`

POST

Create a new appointment

`/api/digitalsignature/capture`

POST

Capture and store a digital signature

`/api/file/upload`

POST

Upload a PDF file

`/api/billing/{billId}/payment`

POST

Apply a payment to a specific bill

`/api/file/download/{fileId}`

GET

Download a previously uploaded file

`/api/patient/{id}/labresults`

GET

Fetch recent lab results for a patient

## Database Schema

The database uses Entity Framework to manage the schema, with tables for `User`, `Appointment`, `DigitalSignature`, `LabResult`, and more. Relationships are defined to support:

-   **User and Appointment**: Each user can have appointments as both a patient and provider.
-   **Digital Signatures and Forms**: Signature data linked securely to forms.
-   **File Storage**: Uploaded files associated with forms and patients.

## Contributing

Contributions are welcome! Please fork the repository, create a new branch, and submit a pull request.

## License

This project is licensed under the MIT License.