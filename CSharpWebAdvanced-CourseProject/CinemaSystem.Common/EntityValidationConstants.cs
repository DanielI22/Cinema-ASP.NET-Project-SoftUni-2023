namespace CinemaSystem.Common
{
    using static CinemaSystem.Common.GeneralApplicationConstants;
    public static class EntityValidationConstants
    {
        public static class Genre
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
        }

        public static class Movie
        {
            public const int TitleMaxLength = 100;
            public const int TitleMinLength = 2;

            public const int DescriptionMaxLength = 1000;
            public const int DescriptionMinLength = 10;

            public const int ReleaseYearMin = 1800;
            public const int ReleaseYearMax = 2300;

            public const int PosterImageUrlMaxLength = 2048;
        }

        public static class Cinema
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;

            public const int AddressMaxLength = 100;
            public const int AddressMinLength = 5;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Review
        {
            public const int TextMaxLength = 500;
            public const int TextMinLength = 1;
        }

        public static class Showtime
        {
            public const int TicketPriceMax = 500;
            public const int TicketPriceMin = 1;
        }

        public static class User
        {
            public const int UsernameMaxLength = 20;
            public const int UsernameMinLength = 1;
        }

        public static class Ticket
        {
            public const int SeatNumberMin = 1;
            public const int SeatNumberMax = CurrentNumberOfSeats;
        }
    }
}
