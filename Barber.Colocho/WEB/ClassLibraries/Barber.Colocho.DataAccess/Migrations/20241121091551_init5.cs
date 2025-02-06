using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Colocho.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dbo",
                table: "ImageService",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Delete",
                schema: "dbo",
                table: "ImageService",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "ImageService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                schema: "dbo",
                table: "ImageService",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dbo",
                table: "ImageService");

            migrationBuilder.DropColumn(
                name: "Delete",
                schema: "dbo",
                table: "ImageService");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "ImageService");

            migrationBuilder.DropColumn(
                name: "Updated",
                schema: "dbo",
                table: "ImageService");
        }
    }
}
