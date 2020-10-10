using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    /// <summary>
    /// The ImageURL partial class is a migration for adding the ImageURL to the movies database.
    /// </summary>
    public partial class ImageUrl : Migration
    {
        /// <summary>
        /// The Up method adds an ImageURL column to the database.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Movie",
                nullable: true);
        }

        /// <summary>
        /// The Down method drops the ImageURL column from the database.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Movie");
        }
    }
}
