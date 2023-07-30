namespace CinemaSystem.Services.Data
{
    using CinemaSystem.Data.Models;
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.Data;
    using CinemaSystem.Web.ViewModels.Movie;
    using CinemaSystem.Web.ViewModels.Showtime;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MovieService : IMovieService
    {
        private readonly CinemaSystemDbContext dbContext;
        private readonly IReviewService reviewService;
        private readonly IConfiguration configuration;

        public MovieService(CinemaSystemDbContext dbContext, IReviewService reviewService , IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.reviewService = reviewService;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<MovieCardViewModel>> GetMovieCardsForMovieIdsAsync(List<string> movieIds)
        {
            return await dbContext.Movies
            .Where(m => movieIds.Contains(m.Id.ToString()))
            .Select(m => new MovieCardViewModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterImageUrl,
                Genres = m.MovieGenres.Select(mg => new GenreViewModel
                {
                    Id = mg.GenreId,
                    Name = mg.Genre.Name
                })
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<MovieCardViewModel>> FilterMoviesAsync(string? searchName, int selectedGenreId)
        {
            IEnumerable<MovieCardViewModel> allMovies = await GetAllMoviesCardAsync();

            // Filter by searchName
            if (!string.IsNullOrEmpty(searchName))
            {
                allMovies = allMovies.Where(m => m.Title.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filter by selectedGenre
            if (selectedGenreId != 0)
            {
                allMovies = allMovies
                    .Where(m => m.Genres.Any(g => g.Id == selectedGenreId))
                    .ToList();
            }

            return allMovies;
        }

        public async Task<IEnumerable<MovieCardViewModel>> GetAllMoviesCardAsync()
        {
            return await dbContext.Movies
                .Where(m => m.isActive)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Select(m => new MovieCardViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterImageUrl,
                    Genres = m.MovieGenres.Select(mg => new GenreViewModel
                    {
                        Id = mg.GenreId,
                        Name = mg.Genre.Name
                    })
                }).ToListAsync();
        }

        public async Task<MovieDetailsViewModel?> GetMovieDetailsModelAsync(string movieId, int pageNumber, int pageSize)
        {
            var reviews = await reviewService.GetMovieReviewsPerPageAsync(movieId, pageNumber, pageSize);
            var totalReviews = await reviewService.GetTotalMovieReviewsCount(movieId);

            MovieDetailsViewModel? movieModel = await dbContext.Movies
           .Where(m => m.Id.ToString() == movieId)
           .Include(m => m.Reviews)
           .Include(m => m.MovieGenres)
           .ThenInclude(mg => mg.Genre)
           .Select(m => new MovieDetailsViewModel
           {
               Id = m.Id,
               Title = m.Title,
               Description = m.Description,
               ReleaseYear = m.ReleaseYear,
               Genres = m.MovieGenres.Select(mg => mg.Genre.Name),
               PosterUrl = m.PosterImageUrl,
               TotalReviews = totalReviews,
               CurrentPage = pageNumber,
               PageSize = pageSize
           }).FirstOrDefaultAsync();

            if (movieModel == null)
            {
                throw new InvalidOperationException("Error loading movie!");
            }

            movieModel.Reviews = reviews;

            return movieModel;
        }

        public async Task<IEnumerable<MovieShowViewModel>> GetAllMoviesAsync()
        {
            return await dbContext.Movies.Where(m => m.isActive)
                 .Include(m => m.MovieGenres)
                 .ThenInclude(mg => mg.Genre)
                 .Select(m => new MovieShowViewModel
                 {
                     Id = m.Id,
                     Title = m.Title,
                     PosterImageUrl = m.PosterImageUrl,
                     ReleaseYear = m.ReleaseYear,
                     Description = m.Description,
                     Genres = m.MovieGenres.Select(mg => new GenreViewModel
                     {
                         Id = mg.GenreId,
                         Name = mg.Genre.Name
                     })
                 }).ToListAsync();
        }

        public async Task AddMovieAsync(MovieAddEditViewModel model)
        {
            Movie movie = new Movie
            {
                Title = model.Title,
                PosterImageUrl = model.PosterImageUrl,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear
            };

            foreach (var genreId in model.GenresId)
            {
                MovieGenre mg = new MovieGenre();
                mg.Movie = movie;
                mg.GenreId = genreId;
                await dbContext.MovieGenre.AddAsync(mg);
            }

            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
        }

        public async Task<MovieAddEditViewModel?> GetEditMovieModelAsync(string id)
        {
            var genres = await dbContext.Genres
               .Where(g => g.isActive)
               .Select(c => new GenreViewModel
               {
                   Id = c.Id,
                   Name = c.Name
               }).ToListAsync();

            return await dbContext.Movies
                .Where(m => m.Id.ToString() == id)
                .Select(m => new MovieAddEditViewModel
                {
                    Title = m.Title,
                    PosterImageUrl = m.PosterImageUrl,
                    Description = m.Description!,
                    ReleaseYear = m.ReleaseYear,
                    GenresId = m.MovieGenres.Select(mg => mg.GenreId).ToList(),
                    Genres = genres
                }).FirstOrDefaultAsync();
        }

        public async Task EditMovieAsync(string id, MovieAddEditViewModel model)
        {
            var movie = await dbContext.Movies
                .Where(m => m.isActive)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                movie.Title = model.Title;
                movie.Description = model.Description;
                movie.ReleaseYear = model.ReleaseYear;
                movie.PosterImageUrl = model.PosterImageUrl;

                List<MovieGenre> movieOldGenres = await dbContext.MovieGenre
                    .Where(mg => mg.MovieId == movie.Id)
                    .ToListAsync();
                dbContext.MovieGenre.RemoveRange(movieOldGenres);

                foreach (var genreId in model.GenresId)
                {
                    MovieGenre mg = new MovieGenre();
                    mg.Movie = movie;
                    mg.GenreId = genreId;
                    await dbContext.MovieGenre.AddAsync(mg);
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteMovieAsync(string id)
        {
            var movie = await dbContext.Movies
             .Where(m => m.isActive)
             .Include(m => m.MovieGenres)
             .ThenInclude(mg => mg.Genre)
             .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                movie.isActive = false;
            }
            await dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<ShowtimeMovieViewModel>> GetAllShowtimeMoviesAsync()
        {
            return await dbContext.Movies
             .Where(m => m.isActive)
             .Select(m => new ShowtimeMovieViewModel()
             {
                 Id = m.Id,
                 Title = m.Title
             }).ToListAsync();
        }

        public async Task AddMovieApiIdAsync(string imdbTag)
        {
            string apiKey = configuration["MyApiSettings:ApiKey"];
            using HttpClient httpClient = new HttpClient();
            string apiUrl = $"http://www.omdbapi.com/?apikey={apiKey}&i={Uri.EscapeDataString(imdbTag)}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if(content != null)
                {
                    MovieApiModel movieModel = JsonConvert.DeserializeObject<MovieApiModel>(content)!;
                    if (movieModel.Title != null && movieModel.Year != null)
                    {
                        await ConvertApiModelToMovie(movieModel);
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid movie format!");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid movie format!");
                }
            }
            else
            {
                throw new InvalidOperationException("No movie is found!");
            }
        }

        public async Task AddMovieApiTitleAsync(string title, int year)
        {
            string apiKey = configuration["MyApiSettings:ApiKey"];
            string apiUrl = $"http://www.omdbapi.com/?apikey={apiKey}&t={Uri.EscapeDataString(title)}";
            if (year != 0)
            {
                apiUrl += $"&y={year}";
            }
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    MovieApiModel movieModel = JsonConvert.DeserializeObject<MovieApiModel>(content)!;
                    if (movieModel.Title != null && movieModel.Year != null)
                    {
                        await ConvertApiModelToMovie(movieModel);
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid movie format!");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid movie format!");
                }
            }
            else
            {
                throw new InvalidOperationException("No movie is found!");
            }
        }

        private async Task ConvertApiModelToMovie(MovieApiModel movieApiModel)
        {
            var movie = new Movie
            {
                Title = movieApiModel.Title,
                ReleaseYear = int.Parse(movieApiModel.Year),
                Description = movieApiModel.Plot,
                PosterImageUrl = movieApiModel.Poster
            };

            var genreNames = movieApiModel.Genre?.Split(", ");

            if (genreNames != null && genreNames.Length > 0)
            {
                foreach (var genreName in genreNames)
                {
                    var existingGenre = await dbContext.Genres
                        .Where(g => g.isActive)
                        .FirstOrDefaultAsync(g => g.Name == genreName);

                    if (existingGenre == null)
                    {
                        var newGenre = new Genre { Name = genreName };
                        MovieGenre movieGenre = new MovieGenre
                        {
                            MovieId = movie.Id,
                            Genre = newGenre,
                        };
                        dbContext.Genres.Add(newGenre);
                        movie.MovieGenres.Add(movieGenre);
                    }
                    else
                    {
                        MovieGenre movieGenre = new MovieGenre
                        {
                            MovieId = movie.Id,
                            Genre = existingGenre,
                        };
                        movie.MovieGenres.Add(movieGenre);
                    }
                }
            }
            dbContext.Movies.Add(movie);
            await dbContext.SaveChangesAsync();
        }
    }
}
