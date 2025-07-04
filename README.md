# BusInfoSys - Bus Information System with LINE Bot Integration

A comprehensive bus information system that provides real-time bus data, route planning, and reminder services through both a web interface and LINE Bot integration.

## 🌟 Features

- **Real-time Bus Information**: Get live bus arrival times and route data
- **LINE Bot Integration**: Interactive bot with rich menu and messaging features
- **Route Planning**: Search and view bus routes with detailed stop information
- **Reminder Service**: Set up notifications for bus arrivals
- **Web Interface**: Modern React frontend with Material-UI components
- **RESTful API**: ASP.NET Core Web API with comprehensive endpoints

## 🏗️ Architecture

The system consists of three main components:

### 1. ASP.NET Core Web API (`/api/`)
- **Framework**: .NET 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT tokens for LINE ID verification
- **External APIs**: Taiwan TDX (Transport Data eXchange) API integration

### 2. React Frontend (`/frontend/`)
- **Framework**: React 19.1.0
- **UI Library**: Material-UI (MUI) 7.1.2
- **Routing**: React Router DOM
- **State Management**: Context API
- **Build Tool**: Create React App

### 3. LINE Bot Integration
- **Webhook Handling**: Real-time message processing
- **Rich Menu Management**: Interactive user interface
- **Message Broadcasting**: Automated notifications
- **LIFF Integration**: LINE Front-end Framework support

## 🚀 Getting Started

### Prerequisites

- Docker and Docker Compose
- .NET 8.0 SDK (for local development)
- Node.js 18+ (for frontend development)

### Quick Start with Docker

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd BusInfoSys
   ```

2. **Start all services**
   ```bash
   docker-compose up -d
   ```

3. **Access the applications**
   - Web API: http://localhost:5289
   - React Frontend: http://localhost:3000
   - SQL Server: localhost:1433
   - ngrok Dashboard: http://localhost:4040

### Individual Service Setup

#### ASP.NET Core API

```bash
cd api
dotnet restore
dotnet build
dotnet run
```

#### React Frontend

```bash
cd frontend
npm install
npm start
```

#### Database Setup

```bash
cd api
dotnet ef database update
```

## 🔧 Configuration

### Environment Variables

Update the following configuration files:

- `api/appsettings.json`: LINE Bot tokens and TDX API credentials
- `api/appsettings.Development.json`: Database connection strings
- `docker-compose.yml`: Service ports and environment variables

### LINE Bot Configuration

1. Set up LINE Bot channel in LINE Developers Console
2. Configure webhook URL using ngrok tunnel
3. Update `appsettings.json` with channel access token

### TDX API Setup

1. Register for TDX API access
2. Obtain client ID and secret
3. Update configuration in `appsettings.json`

## 📚 API Endpoints

### Bus Information
- `GET /api/businfo/BusRoute/{routeName}` - Get bus route information
- `GET /api/businfo/BusStopOfRoute/{routeName}` - Get stops for a route
- `GET /api/businfo/DailyStopTimeTable/{routeName}` - Get daily timetable
- `GET /api/businfo/RealTimeNearStop/{routeName}` - Get real-time bus positions
- `GET /api/businfo/EstimatedTimeOfArrival/{routeName}` - Get arrival estimates

### LINE Bot
- `POST /api/linewebhook` - LINE Bot webhook endpoint
- `POST /api/lineidtokenverify` - Verify LINE ID tokens
- `GET /api/linerichmenus` - Manage rich menus

### Reminders
- `GET /api/remind` - Get user reminders
- `POST /api/remind` - Create new reminder
- `DELETE /api/remind/{id}` - Delete reminder

## 🗄️ Database Schema

### Key Models
- **User**: LINE user information and preferences
- **Bus**: Bus route and vehicle data
- **Stop**: Bus stop locations and details
- **Favorite**: User's favorite routes and stops
- **Remind**: User reminder settings and schedules

## 🔄 Background Services

### ReminderBackgroundService
- Monitors user reminder settings
- Sends notifications via LINE Bot
- Handles scheduled message broadcasting

### TDXTokenService
- Manages TDX API authentication
- Handles token refresh and validation

## 🎨 Frontend Structure

```
frontend/src/
├── components/          # Reusable UI components
├── pages/              # Main application pages
│   ├── BusRouteInput.js
│   ├── BusStopOfRoute.js
│   └── BusRemind.js
├── services/           # API and external services
├── hooks/              # Custom React hooks
├── contexts/           # Global state management
└── utils/              # Utility functions
```

## 🛠️ Development

### Running Tests

```bash
# Frontend tests
cd frontend
npm test

# API tests (if available)
cd api
dotnet test
```

### Building for Production

```bash
# Frontend build
cd frontend
npm run build

# API build
cd api
dotnet publish -c Release
```

## 📋 Development Commands

### Docker Development
```bash
# Start all services
docker-compose up -d

# Start individual services
docker-compose up webapi
docker-compose up react
docker-compose up sqlserver
docker-compose up ngrok
```

### Database Management
```bash
# Create new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Drop database
dotnet ef database drop
```

## 🔐 Security Features

- JWT authentication for LINE ID tokens
- CORS configuration for frontend integration
- Secure API key management
- Input validation and sanitization

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For questions or issues:
- Create an issue in the GitHub repository
- Contact the development team
- Check the documentation in `/docs` folder

## 🔄 Version History

- **v1.0.0**: Initial release with basic bus information features
- **v1.1.0**: Added LINE Bot integration
- **v1.2.0**: Implemented reminder system
- **v1.3.0**: Frontend-backend separation with React

---

**Note**: This system integrates with Taiwan's public transportation data. Ensure you have proper API access and comply with usage terms.