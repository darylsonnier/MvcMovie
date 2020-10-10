using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MvcMovie.Migrations
{
    /// <summary>
    /// The InitialCreate partial class is the base fields for the movie database.
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// The Up method adds the intial columns to the database.
        /// These are ID, Title, ReleaseDate, Genre and Price.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.ID);
                });
        }

        /// <summary>
        /// The Drop method drops the Movie table entirely.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
