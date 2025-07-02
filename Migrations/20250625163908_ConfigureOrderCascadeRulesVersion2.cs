using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEdge.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureOrderCascadeRulesVersion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductVariationId1",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductVariationId1",
                table: "OrderItems",
                column: "ProductVariationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId",
                table: "OrderItems",
                column: "ProductVariationId",
                principalTable: "ProductVariations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId1",
                table: "OrderItems",
                column: "ProductVariationId1",
                principalTable: "ProductVariations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId1",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductVariationId1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductVariationId1",
                table: "OrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId",
                table: "OrderItems",
                column: "ProductVariationId",
                principalTable: "ProductVariations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
