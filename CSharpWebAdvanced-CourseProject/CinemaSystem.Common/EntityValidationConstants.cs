namespace CinemaSystem.Common
{
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
            public const int DescriptionMinLength = 50;

            public const int ReleaseYearMaxLength = 4;
            public const int ReleaseYearMinLength = 4;

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
    }
}
