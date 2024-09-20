   Setup Instructions

1. Extract the Archive

Download and extract the provided .zip archive.
Navigate to the extracted folder, which contains the project files and README.md.

2. Configure the Database
   1.Update the Connection String:
     .Open appsettings.json located in the src/YourProjectName folder.
     .Update the ConnectionStrings:DefaultConnection with your SQL Server instance details.
=============================
     ["ConnectionStrings": {
  "DefaultConnection": "Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;Trusted_Connection=True"
     }]
=============================

2. Apply Migrations:
   .Open a terminal/command prompt in the project directory.
   .Run the following command to apply migrations and set up the database:
==============================
    dotnet ef database update
==============================

3. Run the Application
  .In the terminal, run the following command to start the application:
==============================
   dotnet run
==============================
   The API will be accessible at "https://localhost:5001" or "http://localhost:5000".

4. Test the API
   .Use Postman to test the endpoints.
   .port the provided Postman collection EmploymentSystem.postman_collection.json to get sample requests and responses.

5. Shared Functions
   .Self-registration for both user types.
   .Login.

6. Employer Functions
   .CRUD operations for vacancies.
   .Set a maximum number of allowed applications for vacancies.
   .Post and deactivate vacancies.
   .Set an expiry date for vacancies.
   .View the list of applicants for a vacancy.

7. Applicant Functions
   .Search for vacancies.
   .Apply for vacancies with restrictions (maximum applications and once per 24 hours).

8. System Functions
   .Automatic archiving of expired vacancies.

9. Notes
   .Ensure that the database server is running before starting the application.
   .If you encounter any issues, please check the logs for more details.


Conclusion
Thank you for using the Employment System. For any further questions or issues, please reach out to:
 
- **Name:** Osama Mohammed
- **Phone:** +201017622757
- **Email:** osamazahran960@gmail.com
