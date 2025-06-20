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

### Mocking Framework
- **Use NSubstitute**: The preferred mocking framework for this project is NSubstitute
- Reference NSubstitute documentation: https://nsubstitute.github.io/
- Getting started guide: https://nsubstitute.github.io/docs/2010-01-01-getting-started.html
- Use `Substitute.For<T>()` to create mock objects
- Use `.Returns()` to configure return values for mock methods
- NSubstitute provides a clean, simple syntax for mocking dependencies

### Test Project Setup
When creating test projects:
1. Use `dotnet new xunit` for C# unit test projects
2. Add bUnit package for Blazor component tests: `dotnet add package bunit`
3. Add NSubstitute package for mocking: `dotnet add package NSubstitute`
4. Add project references to the code being tested
5. Ensure tests run with `dotnet test`
6. Tests must pass before code changes are considered complete
7. **Verify test execution**: Always confirm tests actually ran by checking test results output and ensuring test discovery/execution occurred successfully

## Playwright Testing Requirements

When using Playwright MCP tools for browser testing and screenshot capture, follow these specific guidelines:

### Screenshot Capture Rules
- **Format**: Always capture screenshots as uncompressed PNG format, never JPEG
- **Filename Only**: Screenshots must only be captured with a filename, not a full path
- **Raw Parameter**: Always use the `raw: true` parameter to ensure uncompressed PNG format

### Screenshot Directory Locations
Screenshots will always be captured to one of the following directories:
- `$RUNNER_TEMP/artifacts/screenshots/output` (GitHub runner environment)
- `./artifacts/screenshots/output` (VS Code workspace environment)

### Path Handling Requirements
- **Check Both Paths**: Always check both directory locations as either one may be used depending on the environment
- **File Copying**: After capturing a screenshot, copy it from the capture location to the desired location
- **Environment Detection**: The MCP tool will automatically determine which path is available

### Implementation Guidelines
1. Use only filename when calling Playwright screenshot functions
2. Set `raw: true` parameter for uncompressed PNG format
3. After screenshot capture, check both possible directories for the file
4. Copy the screenshot from the found location to your desired destination
5. Handle cases where the file might exist in either location

### Example Usage Pattern
```
1. Capture screenshot with filename only and raw: true
2. Check for file in $RUNNER_TEMP/artifacts/screenshots/output/
3. If not found, check for file in ./artifacts/screenshots/output/
4. Copy found file to desired location
5. Use the copied file for further processing
```

## Additional Notes

- Prioritize .NET 9 specific features and APIs when available
- Suggest migration paths from older .NET versions to .NET 9 when appropriate
- Ensure compatibility with .NET 9 runtime and libraries