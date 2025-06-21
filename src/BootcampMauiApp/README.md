# BootcampMauiApp

A .NET MAUI application that shares services and models with the BootcampWebApp to avoid code duplication.

## Overview

This MAUI app implements the same functionality as the web app with native mobile/desktop UI using XAML:

- **Home Page**: Welcome message
- **Counter Page**: Interactive counter with button
- **Weather Page**: Weather forecast display using shared WeatherService
- **Monkeys Page**: Monkey data display using shared MonkeyService

## Architecture

The app uses the shared `BootcampShared` library containing:

### Models
- `WeatherForecast`: Weather data model
- `Monkey`: Monkey data model

### Services  
- `IWeatherService` / `WeatherService`: Weather data service
- `IMonkeyService` / `MonkeyService`: Monkey data service

## Project Structure

```
BootcampMauiApp/
├── Views/                    # XAML pages
│   ├── HomePage.xaml         # Home page
│   ├── CounterPage.xaml      # Counter page  
│   ├── WeatherPage.xaml      # Weather page
│   └── MonkeysPage.xaml      # Monkeys page
├── Resources/                # App resources
│   ├── AppIcon/              # App icons
│   ├── Splash/               # Splash screen
│   ├── Styles/               # XAML styles
│   └── Images/               # Images
├── Platforms/                # Platform-specific code
│   ├── Android/              # Android platform
│   ├── iOS/                  # iOS platform  
│   ├── MacCatalyst/          # macOS platform
│   └── Windows/              # Windows platform
├── App.xaml                  # Application definition
├── AppShell.xaml            # Shell navigation
└── MauiProgram.cs           # App configuration
```

## Features

### Navigation
Uses Shell navigation with tab bar for easy page switching.

### Dependency Injection
Services are registered in `MauiProgram.cs`:
- HttpClient for network calls
- WeatherService for weather data
- MonkeyService for monkey data  
- All pages registered as transient

### Data Binding
Pages use MVVM pattern with data binding to display service data.

### Shared Business Logic
All business logic is shared with the web app through `BootcampShared` library.

## Pages

### Home Page
- Simple welcome message
- Matches web app home page functionality

### Counter Page  
- Interactive counter with increment button
- Local state management
- Matches web app counter functionality

### Weather Page
- Displays weather forecast data
- Uses shared WeatherService
- Shows loading state during data fetch
- Grid layout for forecast data

### Monkeys Page
- Displays monkey information
- Uses shared MonkeyService  
- Shows loading state during data fetch
- Card layout for monkey data with images

## Requirements

- .NET 9 SDK
- MAUI workload installed (`dotnet workload install maui`)
- Platform-specific SDKs (Android SDK, Xcode, etc.)

## Building

```bash
# Restore packages
dotnet restore

# Build for specific platform
dotnet build -f net9.0-android
dotnet build -f net9.0-ios  
dotnet build -f net9.0-windows10.0.19041.0

# Run on specific platform
dotnet run -f net9.0-android
```

## Testing

The `BootcampMauiApp.Tests` project contains:
- Dependency injection tests
- Page instantiation tests
- Service integration tests

```bash
dotnet test BootcampMauiApp.Tests
```

## Code Sharing

This MAUI app demonstrates effective code sharing:

- **100% shared business logic** via BootcampShared library
- **Platform-specific UI** using XAML
- **Consistent functionality** with web app
- **No code duplication** between web and mobile apps

The shared library approach allows both web and MAUI apps to:
- Use the same data models
- Use the same service implementations  
- Maintain consistent behavior
- Share unit tests for business logic

## Differences from Web App

While functionality is identical, the MAUI app provides:
- Native mobile/desktop experience
- Touch-optimized UI
- Platform-specific features (when needed)
- Offline capability (when implemented)
- Native performance