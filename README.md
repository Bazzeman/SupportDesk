# SupportDesk

SupportDesk is a work-in-progress help desk and ticket management application built with ASP.NET Core. The goal of this project is to showcase modern ASP.NET development practices by implementing a realistic support system with authentication, role-based authorization, real-time communication, and a clean layered architecture.

This project is intended as a portfolio piece and is actively being developed.

## Planned Features

### Authentication & Authorization

- User authentication using **ASP.NET Identity**
- Cookie-based authentication
- Role-Based Access Control (RBAC)
- Separate Client, Staff and Administrator roles

### Ticket Management

- Create support tickets (client)
- Ticket title, description and category
- View open and resolved tickets
- Close and reopen tickets
- Set ticket priority (Staff)
- View all resolved and unresolved tickets (Staff)

### Ticket Conversation

- Conversation thread for every ticket
- Real-time messaging using **SignalR**
- Unread message indicators
- Optional file attachments

### Administration (Optional)

- Manage user accounts
- Assign and change user roles
- Enable or disable user access
- Full Staff permissions

---

## Technology Stack

| Category | Technology |
|----------|------------|
| Framework | ASP.NET Core |
| Architecture | MVC / Service Layer |
| Front-end | Razor Pages |
| Authentication | Cookie Authentication |
| Identity Management | ASP.NET Identity |
| Authorization | Role-Based Access Control (RBAC) |
| Real-time Communication | SignalR |
| Database Access | Entity Framework Core |
| Database | SQLite (development) |
| ORM | Entity Framework Core |
| Styling | Bootstrap |
| Language | C# |
| Version Control | Git & GitHub |

*SQLite is used during development because it requires virtually no setup and allows the application to run immediately after cloning the repository. The data access layer is built with Entity Framework Core, making it straightforward to switch to SQL Server or another relational database in the future.*

---

## Project Goals

This project is intended to showcase experience with:

- ASP.NET Core application development
- Authentication and authorization
- Entity Framework Core
- Clean separation of concerns
- Real-time communication with SignalR
- Database design
- Building maintainable business applications
- Writing clean, readable and scalable code

---

## Future Improvements

Potential additions after the initial release include:

- Action logs
- File attachments
- Ticket assignment
- Email notifications
- Audit logging
- Search and filtering
- Unit and integration tests
- Ticket thread system messages
