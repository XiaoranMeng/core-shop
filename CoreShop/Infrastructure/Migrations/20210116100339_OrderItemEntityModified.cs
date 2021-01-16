using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class OrderItemEntityModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "itemOrdered_ProductName",
                table: "OrderItems",
                newName: "ItemOrdered_ProductName");

            migrationBuilder.RenameColumn(
                name: "itemOrdered_ProductItemId",
                table: "OrderItems",
                newName: "ItemOrdered_ProductItemId");

            migrationBuilder.RenameColumn(
                name: "itemOrdered_PictureUrl",
                table: "OrderItems",
                newName: "ItemOrdered_PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                newName: "itemOrdered_ProductName");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductItemId",
                table: "OrderItems",
                newName: "itemOrdered_ProductItemId");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_PictureUrl",
                table: "OrderItems",
                newName: "itemOrdered_PictureUrl");
        }
    }
}
