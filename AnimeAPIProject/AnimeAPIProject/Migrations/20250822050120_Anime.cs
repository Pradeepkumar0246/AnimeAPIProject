using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeAPIProject.Migrations
{
    /// <inheritdoc />
    public partial class Anime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Genre_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Genre_Id);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Studio_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Studio_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Studio_Year = table.Column<int>(type: "int", nullable: false),
                    Studio_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Studio_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    Anime_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Anime_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anime_Episodes = table.Column<int>(type: "int", nullable: false),
                    Anime_Release_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anime_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anime_Seasons = table.Column<int>(type: "int", nullable: false),
                    Studio_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.Anime_Id);
                    table.ForeignKey(
                        name: "FK_Animes_Studios_Studio_Id",
                        column: x => x.Studio_Id,
                        principalTable: "Studios",
                        principalColumn: "Studio_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeGenres",
                columns: table => new
                {
                    AnimesAnime_Id = table.Column<int>(type: "int", nullable: false),
                    GenresGenre_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeGenres", x => new { x.AnimesAnime_Id, x.GenresGenre_Id });
                    table.ForeignKey(
                        name: "FK_AnimeGenres_Animes_AnimesAnime_Id",
                        column: x => x.AnimesAnime_Id,
                        principalTable: "Animes",
                        principalColumn: "Anime_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeGenres_Genres_GenresGenre_Id",
                        column: x => x.GenresGenre_Id,
                        principalTable: "Genres",
                        principalColumn: "Genre_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWatchedAnimes",
                columns: table => new
                {
                    UsersUser_Id = table.Column<int>(type: "int", nullable: false),
                    WatchedAnimesAnime_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchedAnimes", x => new { x.UsersUser_Id, x.WatchedAnimesAnime_Id });
                    table.ForeignKey(
                        name: "FK_UserWatchedAnimes_Animes_WatchedAnimesAnime_Id",
                        column: x => x.WatchedAnimesAnime_Id,
                        principalTable: "Animes",
                        principalColumn: "Anime_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchedAnimes_Users_UsersUser_Id",
                        column: x => x.UsersUser_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGenres_GenresGenre_Id",
                table: "AnimeGenres",
                column: "GenresGenre_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_Studio_Id",
                table: "Animes",
                column: "Studio_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchedAnimes_WatchedAnimesAnime_Id",
                table: "UserWatchedAnimes",
                column: "WatchedAnimesAnime_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeGenres");

            migrationBuilder.DropTable(
                name: "UserWatchedAnimes");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Studios");
        }
    }
}
