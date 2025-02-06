using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Colocho.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKUAndroid",
                schema: "dbo",
                table: "Plan");

            migrationBuilder.RenameColumn(
                name: "SKUiOS",
                schema: "dbo",
                table: "Plan",
                newName: "SKU");

            migrationBuilder.CreateTable(
                name: "Error",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Error", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Error",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "SKU",
                schema: "dbo",
                table: "Plan",
                newName: "SKUiOS");

            migrationBuilder.AddColumn<string>(
                name: "SKUAndroid",
                schema: "dbo",
                table: "Plan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
