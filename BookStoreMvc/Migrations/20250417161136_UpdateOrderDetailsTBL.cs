using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreMvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderDetailsTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
