using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreMvc.Migrations
{
    /// <inheritdoc />
    public partial class modifiedcartItemTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOrdered",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_OrderDetails_OrderId",
                table: "CartItems",
                column: "OrderId",
                principalTable: "OrderDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_OrderDetails_OrderId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsOrdered",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CartItems");
        }
    }
}
