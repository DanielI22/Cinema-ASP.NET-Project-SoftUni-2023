# Cinema-ASP.NET-Course-Project-SoftUni-2023
Cinema System project for SoftUni ASP.NET Advanced Course


Manual for using
1. Clone the repo.
2. Add connection string and API key in user secrets.
* You can use these for the test
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=CinemaSystemTest;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "MyApiSettings": {
    "ApiKey": "e2c3c97d"
  }
}

3. Apply the migrations.
4. Start the application.
* There are no seeded users!
* To seed an admin - Register a new user with email admin@admin, log out, and restart the application. After logging in you will have access to the admin dashboard.


Cinema System is a web application designed to streamline the movie-going experience by providing users with the ability to browse, select, and reserve seats for movie showtimes at various cinemas. The application offers features such as:

* Browse available cinemas and movies.
* View detailed movie information, including showtimes and plot.
* Reserve seats for desired showtimes.
* User-friendly interface for both customers and administrators.

The application is built using ASP.NET Core, Entity Framework Core, and Bootstrap, providing a responsive and modern user experience. It incorporates features like user authentication, caching, and real-time seat availability updates.


# Features
* Browse Cinemas and Movies: Users can explore available cinemas, view a list of currently showing movies, and access detailed information about each movie.
* Adding a review: Logged-in users can add a review of a movie of their choice to express their opinion about the movie.
* Seat Reservation: Customers can select their desired movie showtimes and reserve seats from an interactive seating chart.
* Real-Time Updates: Users receive real-time updates on seat availability, ensuring an accurate and dynamic booking process.
* Admin Dashboard: Administrators have access to a dedicated dashboard to manage cinemas, movies, and showtimes.
A*PI movie adding: Administrators, apart from manually, can add movies through omdbapi with given title and/or year, or by providing the Imdb movie id.
Security: Role-based authentication ensures secure access to various parts of the application.
