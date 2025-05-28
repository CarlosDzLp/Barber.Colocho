using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Colocho.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class corecttableuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccept",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                schema: "dbo",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "IsPhoneConfirmed",
                schema: "dbo",
                table: "User",
                newName: "IsConfirmedAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmedAccount",
                schema: "dbo",
                table: "User",
                newName: "IsPhoneConfirmed");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccept",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
