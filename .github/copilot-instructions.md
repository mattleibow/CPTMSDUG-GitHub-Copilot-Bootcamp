# GitHub Copilot Instructions

## Prerequisites

This repository requires .NET 9 SDK. If you encounter issues with project creation or building, please ensure .NET 9 SDK is installed on your system and report any setup problems to the user for manual resolution according to your organization's development environment policies.

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

## Testing Requirements

**All code changes must include corresponding unit tests. Testing is mandatory, not optional.**

### Test Framework Selection
- **Blazor Components**: Use bUnit for testing Blazor components and pages
- **C# Code**: Use xUnit for testing C# classes, services, and business logic
- **Test Coverage**: Write at least a couple of tests to demonstrate that changes work correctly

### Project Structure for Tests
- **New Source Projects**: Always create corresponding test projects when adding new source projects
- **Naming Convention**: Test projects should follow the pattern `{ProjectName}.Tests`
- **Target Framework**: Test projects must also target `net9.0`

### bUnit Testing Guidelines
- Reference the bUnit documentation: https://bunit.dev
- Getting started guide: https://bunit.dev/docs/getting-started
- Test Blazor component rendering, interactions, and state changes
- Use TestHost and TestContext for component testing
- Mock external dependencies and services

### xUnit Testing Guidelines
- Reference xUnit documentation: https://xunit.net
- Getting started: https://xunit.net/docs/getting-started/v2/netcore/visual-studio
- Microsoft docs: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit
- Use `[Fact]` for simple tests and `[Theory]` for parameterized tests
- Follow AAA pattern: Arrange, Act, Assert

### Test Project Setup
When creating test projects:
1. Use `dotnet new xunit` for C# unit test projects
2. Add bUnit package for Blazor component tests: `dotnet add package bunit`
3. Add project references to the code being tested
4. Ensure tests run with `dotnet test`
5. Tests must pass before code changes are considered complete
6. **Verify test execution**: Always confirm tests actually ran by checking test results output and ensuring test discovery/execution occurred successfully

## Additional Notes

- Prioritize .NET 9 specific features and APIs when available
- Suggest migration paths from older .NET versions to .NET 9 when appropriate
- Ensure compatibility with .NET 9 runtime and libraries