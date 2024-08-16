using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTCGame.Migrations
{
    /// <inheritdoc />
    public partial class NewsCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecruitsCategoryName",
                table: "NewsCategory",
                newName: "NewsCategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsCategoryName",
                table: "NewsCategory",
                newName: "RecruitsCategoryName");
        }
    }
}
