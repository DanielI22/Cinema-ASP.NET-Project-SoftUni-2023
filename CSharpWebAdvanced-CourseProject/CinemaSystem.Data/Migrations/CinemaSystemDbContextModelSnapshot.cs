﻿// <auto-generated />
using System;
using CinemaSystem.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaSystem.Data.Migrations
{
    [DbContext(typeof(CinemaSystemDbContext))]
    partial class CinemaSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CinemaSystem.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Varna Dubrovnik 8",
                            ImageUrl = "/cinemaImages/cinemaniaVarna.jpg",
                            Name = "Cinemania Varna"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Sofia 33",
                            ImageUrl = "/cinemaImages/cinemaniaSofia.jpg",
                            Name = "Cinemania Sofia"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Plovdiv 15",
                            ImageUrl = "/cinemaImages/cinemaniaPlovdiv.jpg",
                            Name = "Cinemania Plovid"
                        });
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Biography"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Adventure"
                        });
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("PosterImageUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            Description = "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea and by the co-founder who was later squeezed out of the business.",
                            PosterImageUrl = "https://m.media-amazon.com/images/M/MV5BOGUyZDUxZjEtMmIzMC00MzlmLTg4MGItZWJmMzBhZjE0Mjc1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg",
                            ReleaseYear = 2010,
                            Title = "The Social Network"
                        },
                        new
                        {
                            Id = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            Description = "Archaeologist Indiana Jones races against time to retrieve a legendary artifact that can change the course of history.",
                            PosterImageUrl = "https://m.media-amazon.com/images/M/MV5BNDJhODYxYzItOGIwZC00ZTBiLTlmN2MtMjM2MzQyZDVkMGM4XkEyXkFqcGdeQXVyMTUzMDA3Mjc2._V1_SX300.jpg",
                            ReleaseYear = 2023,
                            Title = "Indiana Jones and the Dial of Destiny"
                        });
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.MovieGenre", b =>
                {
                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenre");

                    b.HasData(
                        new
                        {
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            GenreId = 2
                        },
                        new
                        {
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            GenreId = 3
                        },
                        new
                        {
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            GenreId = 4
                        });
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Showtime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TicketPrice")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieId");

                    b.ToTable("Showtimes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CinemaId = 1,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 2,
                            CinemaId = 1,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 3,
                            CinemaId = 1,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 4,
                            CinemaId = 1,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 5,
                            CinemaId = 2,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 6,
                            CinemaId = 2,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 7,
                            CinemaId = 2,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 8,
                            CinemaId = 2,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 9,
                            CinemaId = 3,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 10,
                            CinemaId = 3,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 11,
                            CinemaId = 3,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 12,
                            CinemaId = 3,
                            MovieId = new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                            StartTime = new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 13,
                            CinemaId = 1,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 14,
                            CinemaId = 1,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 15,
                            CinemaId = 1,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 16,
                            CinemaId = 1,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 17,
                            CinemaId = 2,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 18,
                            CinemaId = 2,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 19,
                            CinemaId = 2,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 20,
                            CinemaId = 2,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 21,
                            CinemaId = 3,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 22,
                            CinemaId = 3,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 15m
                        },
                        new
                        {
                            Id = 23,
                            CinemaId = 3,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        },
                        new
                        {
                            Id = 24,
                            CinemaId = 3,
                            MovieId = new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                            StartTime = new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            TicketPrice = 12m
                        });
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShowtimeId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShowtimeId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.MovieGenre", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CinemaSystem.Data.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Review", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CinemaSystem.Data.Models.ApplicationUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Showtime", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.Cinema", "Cinema")
                        .WithMany("Showtimes")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CinemaSystem.Data.Models.Movie", "Movie")
                        .WithMany("Showtimes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Ticket", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.Showtime", "Showtime")
                        .WithMany("Tickets")
                        .HasForeignKey("ShowtimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CinemaSystem.Data.Models.ApplicationUser", "User")
                        .WithMany("ReservedTickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Showtime");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CinemaSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("ReservedTickets");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Cinema", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Genre", b =>
                {
                    b.Navigation("MovieGenres");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Movie", b =>
                {
                    b.Navigation("MovieGenres");

                    b.Navigation("Reviews");

                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("CinemaSystem.Data.Models.Showtime", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
