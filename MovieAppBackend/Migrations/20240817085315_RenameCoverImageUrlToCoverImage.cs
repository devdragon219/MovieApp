using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class RenameCoverImageUrlToCoverImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImageUrl",
                table: "Movies",
                newName: "CoverImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Movies",
                newName: "CoverImageUrl");
        }
    }
}
