using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Colocho.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeletedAccount",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeletedAccount",
                schema: "dbo",
                table: "User");
        }
    }
}
