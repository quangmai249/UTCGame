using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTCGame.Migrations
{
    /// <inheritdoc />
    public partial class FolderMediaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderMedia");

            migrationBuilder.CreateTable(
                name: "FolderMediaModel",
                columns: table => new
                {
                    FolderMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderMediaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvtive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderMediaModel", x => x.FolderMediaID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderMediaModel");

            migrationBuilder.CreateTable(
                name: "FolderMedia",
                columns: table => new
                {
                    FolderMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderMediaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvtive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderMedia", x => x.FolderMediaID);
                });
        }
    }
}
