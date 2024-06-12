using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore_Nhom2_Web2_API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Remaining",
                table: "Shoe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "Shoe");
        }
    }
}
