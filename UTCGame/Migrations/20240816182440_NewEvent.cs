using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTCGame.Migrations
{
    /// <inheritdoc />
    public partial class NewEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewEvent",
                columns: table => new
                {
                    NewEventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewEventTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewEventDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewEventDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewEvent", x => x.NewEventID);
                    table.ForeignKey(
                        name: "FK_NewEvent_FolderMediaModel_FolderMediaID",
                        column: x => x.FolderMediaID,
                        principalTable: "FolderMediaModel",
                        principalColumn: "FolderMediaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewEvent_NewsCategory_NewsCategoryID",
                        column: x => x.NewsCategoryID,
                        principalTable: "NewsCategory",
                        principalColumn: "NewsCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewEvent_FolderMediaID",
                table: "NewEvent",
                column: "FolderMediaID");

            migrationBuilder.CreateIndex(
                name: "IX_NewEvent_NewsCategoryID",
                table: "NewEvent",
                column: "NewsCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewEvent");
        }
    }
}
