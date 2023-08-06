# Cinema-ASP.NET-Course-Project-SoftUni-2023
Cinema System project for SoftUni ASP.NET Advanced Course


Manual for using
Add connection string and API key in user secrets.
Use these for the test
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=CinemaSystemTest;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "MyApiSettings": {
    "ApiKey": "e2c3c97d"
  }
}
Start the application.
There are no seeded users.
To seed an admin - Register a new user with email admin@admin, log out, and restart the application if you want to seed admin.
