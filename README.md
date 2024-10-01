Gift of the Givers Foundation Web Application
Overview
The **Gift of the Givers Foundation Web Application** is designed to facilitate donations and provide user-friendly access to foundation services. This ASP.NET MVC web application supports user registration, login, donation submissions, and profile management. The application is developed using C#, ASP.NET MVC, and an Azure SQL database for data storage.

Key Features
o	User Registration and Authentication: Users can register, log in, and manage their profiles securely.
o	Donation Form: A form to submit details about donations, which are saved in the database.
o	Profile Management: Users can update their personal information and view their donation history.

Installation and Setup Instructions
Follow these steps to set up the application locally or on a server.
Prerequisites
1.	NET SDK: Make sure you have the .NET SDK installed (version 5.0 or later). You can download it [here](https://dotnet.microsoft.com/download).
2.	Visual Studio 2022 or later: Install Visual Studio with the ASP.NET and web development workload.
3.	SQL Server or Azure SQL Database: You will need a SQL Server or Azure SQL Database to store application data.
Cloning the Repository
1. Clone the repository or download it as a ZIP file and extract it: 
https://github.com/ST10221866/The_Gift_of_the_Givers.git
2. Open the solution file (`GiftOfTheGivers.sln`) in Visual Studio.
Configuration
1.	Database Connection: Update the connection string in the `appsettings.json` file to match your SQL Server or Azure SQL Database settings:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server;Database=GiftOfTheGivers;User Id=your-username;Password=your-password;"
}
2.	Authentication: If you are using external authentication (like Identity or JWT), make sure that the authentication options are correctly configured in `Startup.cs`. Update the `ConfigureServices` method if necessary to suit your authentication system.

Database Setup
Update-Database
This will create the necessary tables for user profiles and donations in the database.
Running the Application

1.	Press `F5` or click on **Start Debugging** in Visual Studio to run the application.
2.	The application will launch in your default browser at `https://localhost:5001/` or `https://localhost:5000/` for HTTP.
How to Use
1.	Register: Create a new account using the registration form.
2.	Login: Log in with your credentials.
3.	Submit a Donation: Navigate to the donation form, fill in the required fields, and submit your donation.
4.	Update Profile: Go to the profile page and click "Edit" to update your personal details.
5.	Admin Functions: Admin users can manage other users' donations and profiles (requires admin privileges).

Technologies Used

1.	ASP.NET Core MVC: Web application framework for building dynamic, data-driven applications.
2.	C#: Programming language used for back-end logic.
o	Entity Framework Core: ORM (Object-Relational Mapper) for database access and management.
o	Azure SQL Database: Cloud database used for data storage.
o	Bootstrap: Front-end framework for responsive design and layout. 
 Troubleshooting
1. Common Errors:
•	Database Connection Issues: Ensure your connection string is correct and the database is running.
•	-Authentication Issues: Make sure the authentication schemes in `Startup.cs/ program.cs` are configured properly.
•	Session Management: Verify that the session middleware is properly set up in `Startup.cs/ program.cs`.

2. Logs and Error Handling:
   - Application errors and stack traces are logged to the console when running in development mode.
   - Use the Developer Exception Page for detailed error information in the browser.

•	Future Improvements
1. Email Notifications: Send email confirmations when a donation is submitted.
2. Data Analytics: Integrate dashboards to display donation statistics and reports.

