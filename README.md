# OPort (Sample ASP.NET Core Web API)

This project is a sample **ASP.NET Core Web API** application demonstrating user authentication, role management, and CRUD operations on different entities such as **Projects**, **Resumes**, and **Specialities**. The application also supports file uploads and downloads, providing a comprehensive example of how to structure a multi-layered .NET web application.

## Table of Contents

1. [Project Description](#project-description)  
2. [Features & Functionality](#features--functionality)  
   - [Authentication & Authorization](#authentication--authorization)  
   - [Projects](#projects)  
   - [Resumes](#resumes)  
   - [Specialities](#specialities)  
   - [File Uploads](#file-uploads)  
3. [Prerequisites](#prerequisites)  
4. [Installing & Running](#installing--running)  
5. [Usage Instructions](#usage-instructions)  

---

## Project Description

**OPort** is an ASP.NET Core Web API designed to showcase how to build a modular back-end system using:

- [EF Core](https://learn.microsoft.com/en-us/ef/core/) with **Microsoft SQL Server** for data persistence.
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) for user and role management.
- [AutoMapper](https://automapper.org/) for model-to-DTO mapping.
- Swagger for API documentation and testing.

---

## Features & Functionality

### Authentication & Authorization

The system uses **ASP.NET Core Identity** to manage users and roles:

- **SignUp**: Create a new user with a specified password.
- **SignIn**: Authenticate a user with username/password.
- **SignOut**: Log out a user.
- **UpdateUserRole**: Assign a role to a specified user.
- **AddRole**: Create a new role in the database.
- **FetchRoles**: Retrieve existing roles.
- **GetUserInfo**: Fetch user information by user ID.

> **Controller**: `AuthController`

### Projects

CRUD operations on projects, including uploading a project cover file.  
**Endpoints**:

- **CreateProject** (`POST /api/Projects/create`): Create a new project record with an optional cover file.
- **GetAsync** (`GET /api/Projects/{id}`): Get a project by its GUID.
- **FetchByResumeIdAsync** (`GET /api/Projects/fetch-by-resume/{resumeId}`): Fetch all projects related to a specific resume.
- **UpdateAsync** (`PUT /api/Projects/{id}`): Update a project’s details.
- **DeleteAsync** (`DELETE /api/Projects/{id}`): Delete a project by its GUID.

> **Controller**: `ProjectsController`

### Resumes

CRUD operations for resumes, including the ability to fetch resumes by user ID.  
**Endpoints**:

- **Add** (`POST /api/Resumes/add`): Create a new resume.
- **Delete** (`DELETE /api/Resumes/{id}`): Delete a resume by its GUID.
- **Update** (`PUT /api/Resumes/update`): Update a resume’s details.
- **FetchByUserId** (`GET /api/Resumes/fetch-by-user/{id}`): Fetch resumes belonging to a specific user.
- **Fetch** (`GET /api/Resumes/fetch`): Fetch resumes filtered by criteria.
- **Get** (`GET /api/Resumes/{id}`): Fetch a single resume by its GUID.

> **Controller**: `ResumesController`

### Specialities

CRUD operations for managing user or resume specialities.  
**Endpoints**:

- **AddAsync** (`POST /api/Specialities/add`): Create a new speciality.
- **Delete** (`DELETE /api/Specialities/{id}`): Delete a speciality by its integer ID.
- **Get** (`GET /api/Specialities/{id}`): Get a speciality by its ID.
- **Fetch** (`GET /api/Specialities/fetch`): Fetch a list of all specialities.

> **Controller**: `SpecialitiesController`

### File Uploads

The system supports file uploads related to projects. The uploaded files are stored on the server’s file system, and the relevant file metadata (path, name, etc.) is saved in the database.

- **UploadFile** (`POST /api/Files/upload/{projectId}`): Upload a file and associate it with a specific project.
- **GetFile** (`GET /api/Files/{fileId}`): Download a file by its GUID.

> **Controller**: `FilesController`

---

## Prerequisites

Ensure you have met the following requirements:

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) instance (local or remote)
- An IDE or editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

---

## Installing & Running

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/your-repo/OPort.git
   cd OPort
2. **Configure the Connection String**  

   - Open the `appsettings.json` or `appsettings.Development.json` file.  
   - Locate the `"ConnectionStrings"` section.  
   - Update the `"default"` connection string to match your SQL Server setup:  

     Example:  
     ```json
     "ConnectionStrings": {
       "default": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```  
     Replace `YOUR_SERVER` and `YOUR_DATABASE` with your server and database names.

3. **Restore & Build the Project**  

   Run the following commands to restore dependencies and build the project:  
   ```bash
   dotnet restore
   dotnet build
   
4. **Apply EF Core Migrations**  

   - Apply the migrations using:  
     ```bash
     dotnet ef database update
     ```  
5. **Run the Application**  

   - Start the application with:  
     ```bash
     dotnet run
     ```  
   - The application will start on the configured port (typically `https://localhost:5001` or `http://localhost:5000` for development).

6. **Open Swagger UI**  

   - Navigate to `https://localhost:5001/swagger` (or the corresponding port).

---

## Usage Instructions

### Authentication Flow  

- **SignUp**: Use the `POST /api/Auth/signup` endpoint to create a user, providing `firstName`, `lastName`, `password`, and `email`.  
- **SignIn**: Use the `POST /api/Auth/signin` endpoint to log in with the created user credentials.

### Role Management  

- **AddRole**: Use `POST /api/Auth/add-role` to create a new role.  
- **UpdateUserRole**: Use `PUT /api/Auth/update-role` to assign a role to a user.

### File Upload  

- **FilesController**: Use `POST /api/Files/upload/{projectId}` to upload a file for a given project ID. Include a form-data request with the key `file` pointing to the file to be uploaded.

### Common Endpoints  

- **Projects**: CRUD operations at `/api/Projects`.  
- **Resumes**: CRUD operations at `/api/Resumes`.  
- **Specialities**: CRUD operations at `/api/Specialities`.  
