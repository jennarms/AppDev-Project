using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroLayag.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCanceledToPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Passengers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Passengers");
        }
    }
}
