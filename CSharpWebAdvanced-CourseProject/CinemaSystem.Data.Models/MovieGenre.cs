﻿namespace CinemaSystem.Data.Models
{
    public class MovieGenre
    {
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;
    }
}
