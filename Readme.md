# Notify System

This is a notification system built with ASP.NET Core. It supports various notification channels such as Email, In-App, Push, and SMS.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL](https://www.mysql.com/)

### Setup

1. Clone the repository:

   ```sh
   git clone https://github.com/NGOVANMANH/NotificationBackend notify
   cd notify
   ```

2. Configure the database connection string in `appsettings.json`:

   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=NotifyDatabase;User=YOUR_USER;Password=YOUR_PASSWORD"
     },
     "Environment": "Development",
     "Mail": {
       "Host": "YOUR_HOST",
       "Port": 587,
       "Email": "YOUR_EMAIL",
       "Password": "YOUR_GMAIL_APP_PASSWORD"
     }
   }
   ```

3. Apply migrations to set up the database:

   ```sh
   dotnet ef database update
   ```

4. Run the application:
   ```sh
   dotnet run
   ```

### Project details

##### Controllers

- **NotificationsController**: Handles notification-related API endpoints.
- **PreferencesController**: Manages user preferences for notifications.
- **UsersController**: Manages user-related operations.

##### Services

- **EmailService**: Sends emails using SMTP.
- **NotificationService**: Manages the creation and distribution of notifications.
- **PreferenceService**: Handles user preferences for notifications.

##### Repositories

- **PreferenceRepository**: Manages CRUD operations for user preferences.
- **UserRepository**: Manages CRUD operations for users.
- **NotificationRepository**: Manages CRUD operations for notifications.

##### Subscribers

- **EmailNotificationSubscriber**: Handles email notifications.
- **InAppNotificationSubscriber**: Handles in-app notifications.

##### Publishers

- **NotificationPublisher**: Manages the subscription and notification of subscribers.

##### Middlewares

- **GlobalExceptionHandler**: Handles global exceptions and returns appropriate responses.
