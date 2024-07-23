# Simple Chat Application.


## Description

- Users should be able to create chats.
- Connect to exist and delete if the user created the chat. 


## Tech Stack

- **ASP.NET Core (Web API)**: The backend framework for creating the API.
- **Entity Framework Core**: The ORM (Object-Relational Mapper) for interacting with the database using a code-first approach.
- **SignalR**: A library for adding real-time web functionality, allowing for live messaging within chats.
- **.NET 8**: The framework version used for this application.


## Architecture

The application follows a 3-tier architecture:

1. **WEB**: Handles HTTP requests, routing, and interacts directly with clients.
2. **Data access layer**: anages database interactions using Entity Framework Core or another ORM.
3. **Bussiness logic layer**: Implements business rules, validation, and workflows.

**Test**: Includes unit tests, integration tests for testing purposes.
