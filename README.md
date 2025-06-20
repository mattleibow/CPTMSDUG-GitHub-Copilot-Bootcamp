# Testing Bootcamp

A comprehensive .NET 9 Blazor web application demonstrating various web development concepts and testing practices.

## About

This project is a Blazor Server application built with .NET 9 that showcases different types of pages and functionality including interactive components, data display, and API integration.

## Features

The application includes the following pages:

### Home Page
The main landing page with a welcome message.

![Home Page](docs/images/home-page.png)

### Counter Page
An interactive counter component demonstrating Blazor's client-side interactivity.

![Counter Page](docs/images/counter-page.png)

### Weather Page
A data display page showing weather forecast information.

![Weather Page](docs/images/weather-page.png)

### Monkeys Page
A comprehensive page displaying monkey species data with detailed cards and information.

![Monkeys Page](docs/images/monkeys-page.png)

## Getting Started

### Prerequisites

- .NET 9 SDK

### Running the Application

1. Clone the repository
2. Navigate to the src/BootcampWebApp directory
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to the URL shown in the console (typically https://localhost:5001 or http://localhost:5000)

### Running Tests

To run the unit tests:

```bash
dotnet test
```

## Technology Stack

- .NET 9
- Blazor Server
- Bootstrap CSS
- xUnit (for testing)
- bUnit (for Blazor component testing)
- NSubstitute (for mocking)

## Project Structure

```
src/
├── BootcampWebApp/           # Main web application
│   ├── Components/           # Blazor components and pages
│   ├── Models/              # Data models
│   ├── Services/            # Business services
│   └── wwwroot/             # Static web assets
└── BootcampWebApp.Tests/    # Unit tests
```