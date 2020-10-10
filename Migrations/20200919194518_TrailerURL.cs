using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    /// <summary>
    /// The TrailerURL class is a migration for adding a TrailerURL field to the movies database.
    /// </summary>
    public partial class TrailerURL : Migration
    {
        /// <summary>
        /// The Up method adds a column for the TrailerURL to the database.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrailerURL",
                table: "Movie",
                nullable: true);
        }

        /// <summary>
        /// The Down method drops a column for the TrailerURL from the database.
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailerURL",
                table: "Movie");
        }
    }
}
