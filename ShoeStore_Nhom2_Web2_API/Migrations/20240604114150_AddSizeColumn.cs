using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore_Nhom2_Web2_API.Migrations
{
    /// <inheritdoc />
    public partial class AddSizeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Shoe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Shoe");
        }
    }
}
