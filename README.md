# Microservices API Solution

This solution demonstrates a microservices architecture using ASP.NET Core, consisting of three main components: MainAPI, CustomerAPI, and OrdersAPI. The project is built using .NET 8 and leverages the Aspire framework for distributed application development.

## Project Structure

- **MainAPI**: The central API that orchestrates requests to both CustomerAPI and OrdersAPI.
- **CustomerAPI**: Manages customer-related operations and data.
- **OrdersAPI**: Handles order-related functionalities.
- **ApiTests**: Contains unit tests for CustomerAPI and OrdersAPI.

## Key Features

1. **Microservices Architecture**: The solution is divided into separate services, each responsible for specific functionality.
2. **API Key Authentication**: All APIs are secured using API key authentication.
3. **Aspire Integration**: Utilizes the Aspire framework for easier management of distributed applications.
4. **Swagger Documentation**: Each API is documented using Swagger for easy testing and integration.
5. **Unit Testing**: Includes unit tests to ensure the reliability of the APIs.
6. **Multiple HTTP Methods**: MainAPI supports both GET and POST methods for fetching combined data from CustomerAPI and OrdersAPI.

## Getting Started

1. Ensure you have .NET 8 SDK installed.
2. Clone the repository.
3. Open the Solution in VS 2022
4. Set MainAPI.AppHost as default startup.
5. Run the application. You will see the Aspire page with the 3 Swagger endpoints.
6. Select MainAPI and submit a request using either the GET or POST method.

The api key is magic-key

## API Endpoints

MainAPI provides two endpoints for fetching combined data from CustomerAPI and OrdersAPI:

1. GET /main: Fetches customers and orders data.
2. POST /main/MainPost: Also fetches customers and orders data, but uses a POST method.

Both endpoints require an API key to be provided in the X-API-Key header.

## Running Tests

To run the unit tests, use the following command:

```
dotnet test ApiTests/ApiTests.csproj
```

Alternatively, run the tests in VS Test Explorer.

## Configuration

API keys and other configuration settings can be found in the `appsettings.json` file of each project.

## Future Enhancements

- Implement database integration for persistent storage.
- Add more comprehensive error handling and logging.
- Implement caching mechanisms for improved performance.
- Expand test coverage to include integration tests.

## License

This project is licensed under the MIT License.
