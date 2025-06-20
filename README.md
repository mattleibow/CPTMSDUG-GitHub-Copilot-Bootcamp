# Testing Bootcamp Web Application

A comprehensive testing bootcamp web application built with .NET 9 and Blazor Server. This application demonstrates various testing concepts and includes multiple pages showcasing different features.

## Overview

The Testing Bootcamp Web Application is a Blazor Server application that provides an interactive environment for learning and practicing testing concepts. The application features a modern, responsive design with a clean navigation interface.

## Pages

The application consists of four main pages:

### 1. Home Page
The landing page that welcomes users to the application.

![Home Page](docs/images/home-page.png)

### 2. Counter Page
An interactive counter demonstration with a button that increments a value. This page showcases component interactivity and state management.

![Counter Page](docs/images/counter-page.png)

### 3. Weather Page
A data display page showing weather forecast information in a tabular format. This demonstrates data binding and async operations.

![Weather Page](docs/images/weather-page.png)

### 4. Monkeys Page
A rich content page displaying information about various monkey species from around the world. Features card-based layout with images, descriptions, and population data.

![Monkeys Page](docs/images/monkeys-page.png)

## Technologies Used

- .NET 9
- Blazor Server
- C# 
- HTML/CSS
- Bootstrap for styling

## Getting Started

1. Clone the repository
2. Ensure you have .NET 9 SDK installed
3. Navigate to the `src/BootcampWebApp` directory
4. Run `dotnet build` to build the application
5. Run `dotnet run` to start the application
6. Open your browser and navigate to the displayed URL (typically `http://localhost:5086`)

## Testing

The application includes comprehensive unit tests using:
- xUnit for C# unit testing
- bUnit for Blazor component testing
- NSubstitute for mocking

To run tests:
```bash
dotnet test
```

## Project Structure

```
src/
├── BootcampWebApp/           # Main web application
│   ├── Components/
│   │   ├── Pages/           # Blazor pages
│   │   └── Layout/          # Layout components
│   ├── Models/              # Data models
│   ├── Services/            # Application services
│   └── wwwroot/            # Static web assets
└── BootcampWebApp.Tests/    # Unit tests
    └── Components/          # Component tests
```