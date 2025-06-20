# GitHub Copilot Instructions

## Installation Requirements

Before working with this repository, ensure .NET 9 SDK is installed:

### Check Current Installation
```bash
dotnet --version
```

### Install .NET 9 SDK if not available
**Linux/macOS**:
```bash
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 9.0
```

**Windows**:
```powershell
Invoke-WebRequest -Uri https://dot.net/v1/dotnet-install.ps1 -OutFile dotnet-install.ps1; .\dotnet-install.ps1 -Channel 9.0
```

**Verify installation**:
```bash
dotnet --version
```

## .NET Version Requirements

- **Always use .NET 9**: When creating new projects, writing code, or suggesting solutions, always target the latest .NET 9 framework.
- **Target Framework**: Use `net9.0` as the target framework in all project files (.csproj).
- **SDK Version**: Use .NET 9 SDK for all development activities.

## Code Quality Requirements

- **Compilation**: Ensure all code suggestions and generated code compile successfully.
- **Build Verification**: Code must build without errors before being considered complete.
- **Best Practices**: Follow .NET 9 best practices and latest language features.

## Project Setup Guidelines

When creating new .NET projects:
1. Use `dotnet new` commands with .NET 9 templates
2. Verify the target framework is set to `net9.0`
3. Test compilation with `dotnet build` before finalizing
4. Use the latest C# language version supported by .NET 9

## Additional Notes

- Prioritize .NET 9 specific features and APIs when available
- Suggest migration paths from older .NET versions to .NET 9 when appropriate
- Ensure compatibility with .NET 9 runtime and libraries