using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTCGame.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsActiveinFolderMediaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvtive",
                table: "FolderMediaModel",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "FolderMediaModel",
                newName: "IsAvtive");
        }
    }
}
