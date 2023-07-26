using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrazione3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Purchases_PurchaseId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_PurchaseId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "CartItems");

            migrationBuilder.AddColumn<string>(
                name: "CoverVideogame",
                table: "VideoGames",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartItemId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CartItemId",
                table: "Purchases",
                column: "CartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_CartItems_CartItemId",
                table: "Purchases",
                column: "CartItemId",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_CartItems_CartItemId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CartItemId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CoverVideogame",
                table: "VideoGames");

            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_PurchaseId",
                table: "CartItems",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Purchases_PurchaseId",
                table: "CartItems",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");
        }
    }
}
