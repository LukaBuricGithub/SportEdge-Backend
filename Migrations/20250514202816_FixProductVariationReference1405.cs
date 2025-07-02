using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEdge.API.Migrations
{
    /// <inheritdoc />
    public partial class FixProductVariationReference1405 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVariations_ProductVariationId1",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductVariationId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductVariationId1",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductVariationId1",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductVariationId1",
                table: "CartItems",
                column: "ProductVariationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVariations_ProductVariationId1",
                table: "CartItems",
                column: "ProductVariationId1",
                principalTable: "ProductVariations",
                principalColumn: "Id");
        }
    }
}
