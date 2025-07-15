# WTW Tech Lead Challenge

This project is a full-stack application for request management system with a .NET 8 Web API backend and an Angular 19 frontend.

## Project Structure

```
wtw_tech_lead_challenge/
├── wtw_server/          # Backend .NET 8 Web API
│   ├── wtw_server/      # Main Web API project
│   ├── BLL/             # Business Logic Layer
│   ├── DAL/             # Data Access Layer
│   ├── Entity/          # Data models and DTOs
│   ├── Tools/           # Utility classes
│   └── wtw_server.Tests/ # Unit tests
├── wtw_ui/              # Frontend Angular 19 application
└── wtw_script_db/       # Database scripts
```

## Prerequisites

Before running this project locally, ensure you have the following installed:

- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** - Required for backend
- **[Node.js](https://nodejs.org/)** (version 18 or higher) - Required for frontend
- **[Angular CLI](https://angular.io/cli)** - Install with `npm install -g @angular/cli`
- **[SQL Server](https://www.microsoft.com/sql-server)** or **SQL Server Express** - For database

## Database Setup

1. **Create the database:**
   - Open SQL Server Management Studio (SSMS)
   - Create a new database named `WTW`

2. **Execute database scripts:**
   ```bash
   # Navigate to database scripts folder
   cd wtw_script_db
   
   # Execute the main script that creates tables and inserts sample data
   # Run "Query to execute all.sql" in SSMS against the WTW database
   ```

3. **Configure connection string:**
   - Update the connection string in `wtw_server/wtw_server/appsettings.json`
   - Replace the connection string with your SQL Server details:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=WTW;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

## Running the Backend (.NET Web API)

1. **Navigate to the server directory:**
   ```bash
   cd wtw_server
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Run the application:**
   ```bash
   cd wtw_server
   dotnet run
   ```

5. **Access the API:**
   - **Swagger UI:** https://localhost:7168/swagger
   - **HTTP endpoint:** http://localhost:5013
   - **HTTPS endpoint:** https://localhost:7168

## Running the Frontend (Angular)

1. **Navigate to the UI directory:**
   ```bash
   cd wtw_ui
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Start the development server:**
   ```bash
   npm start
   # or
   ng serve
   ```

4. **Access the application:**
   - **Frontend:** http://localhost:4200

## Running Tests

### Backend Tests
```bash
cd wtw_server
dotnet test
```

### Frontend Tests
```bash
cd wtw_ui
npm test
```

## API Endpoints

The backend provides the following main endpoints:

- **GET** `/api/Request` - Get all requests
- **GET** `/api/Request/{id}` - Get request by ID
- **POST** `/api/Request` - Create new request
- **DELETE** `/api/Request/{id}` - Delete request
- **POST** `/api/Request/filter` - Filter requests
- **GET** `/api/Request/search` - Search requests by JSON property

## Request Types

The system supports three types of requests:

1. **Vacation Request** - For time off requests
2. **Loan Request** - For financial loan requests  
3. **Permission Request** - For temporary leave permissions

Each request type has its own JSON schema validation.

## Development Environment Configuration

### Backend Configuration
- **Development:** Uses `appsettings.Development.json`
- **Production:** Uses `appsettings.Production.json`
- **CORS:** Enabled for all origins in development

### Frontend Configuration
- **Environment:** Configuration in `src/environment/environment.ts`
- **API Base URL:** Update to match your backend URL

## Project Architecture

### Backend (.NET 8)
- **Architecture:** Clean Architecture with separation of concerns
- **Layers:** 
  - **wtw_server:** Web API controllers and configuration
  - **BLL:** Business logic and validation
  - **DAL:** Data access and Entity Framework
  - **Entity:** Data models, DTOs, and schemas
  - **Tools:** Utility classes
- **Database:** Entity Framework Core with SQL Server
- **Testing:** xUnit with Moq for unit testing

### Frontend (Angular 19)
- **Architecture:** Component-based with services
- **UI Framework:** Angular Material
- **State Management:** Services with RxJS
- **Routing:** Angular Router
- **HTTP Client:** Angular HttpClient for API communication

## Troubleshooting

### Common Issues

1. **Database Connection Issues:**
   - Verify SQL Server is running
   - Check connection string in appsettings.json
   - Ensure database exists and scripts have been executed

2. **Port Conflicts:**
   - Backend default ports: 5013 (HTTP), 7168 (HTTPS)
   - Frontend default port: 4200
   - Change ports in launchSettings.json (backend) or use `ng serve --port XXXX` (frontend)

3. **CORS Issues:**
   - Ensure backend CORS is configured correctly
   - Check frontend API base URL configuration

4. **Build Errors:**
   - Run `dotnet clean` and `dotnet build` for backend
   - Delete `node_modules` and run `npm install` for frontend

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Run tests to ensure everything works
5. Submit a pull request

## License

This project is part of the WTW Tech Lead Challenge.