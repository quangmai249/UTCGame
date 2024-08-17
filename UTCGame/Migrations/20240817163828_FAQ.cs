using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTCGame.Migrations
{
    /// <inheritdoc />
    public partial class FAQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    FAQ_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FAQ_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQ_Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.FAQ_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQ");
        }
    }
}
