using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.API.InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sell",
                table: "InventoryDetails",
                newName: "IsSell");

            migrationBuilder.RenameColumn(
                name: "Buy",
                table: "InventoryDetails",
                newName: "IsBuy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSell",
                table: "InventoryDetails",
                newName: "Sell");

            migrationBuilder.RenameColumn(
                name: "IsBuy",
                table: "InventoryDetails",
                newName: "Buy");
        }
    }
}
