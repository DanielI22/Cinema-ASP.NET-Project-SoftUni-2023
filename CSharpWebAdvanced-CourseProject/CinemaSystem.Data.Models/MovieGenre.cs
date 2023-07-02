using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystem.Data.Models
{
    public class MovieGenre
    {
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;
    }
}
