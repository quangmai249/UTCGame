using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTCGame.Migrations
{
    /// <inheritdoc />
    public partial class GameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamePrice = table.Column<float>(type: "real", nullable: false),
                    GameType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GamePlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsGameActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Game_EmployeeModel_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeModel",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_FolderMediaModel_FolderMediaID",
                        column: x => x.FolderMediaID,
                        principalTable: "FolderMediaModel",
                        principalColumn: "FolderMediaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamePlatformID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamePlatformName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamePlatformLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => x.GamePlatformID);
                });

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    GameTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.GameTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_EmployeeID",
                table: "Game",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_FolderMediaID",
                table: "Game",
                column: "FolderMediaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "GameType");
        }
    }
}
