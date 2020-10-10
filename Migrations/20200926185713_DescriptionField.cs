using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    /// <summary>
    /// The DescriptionField partial class is a migration to add a description field to the database context for a movie.
    /// </summary>
    public partial class DescriptionField : Migration
    {
        /// <summary>
        /// The Up method adds a column for a movie description to the database.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movie",
                nullable: true);
        }

        /// <summary>
        /// The Down method drops the column for a movie description from the database.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movie");
        }
    }
}
