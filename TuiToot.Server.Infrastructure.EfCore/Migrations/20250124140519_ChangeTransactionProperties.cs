using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuiToot.Server.Infrastructure.EfCore.Migrations
{
    public partial class ChangeTransactionProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryAddress_DeliveryAddressId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_BagType_BagTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BagType",
                table: "BagType");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "BagType",
                newName: "BagTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_OrderId",
                table: "Transactions",
                newName: "IX_Transactions_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_OrderId",
                table: "Products",
                newName: "IX_Products_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BagTypeId",
                table: "Products",
                newName: "IX_Products_BagTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryAddressId",
                table: "Orders",
                newName: "IX_Orders_DeliveryAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ApplicationUserId",
                table: "Orders",
                newName: "IX_Orders_ApplicationUserId");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductCost",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BagTypes",
                table: "BagTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryAddress_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId",
                principalTable: "DeliveryAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BagTypes_BagTypeId",
                table: "Products",
                column: "BagTypeId",
                principalTable: "BagTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                table: "Transactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryAddress_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BagTypes_BagTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BagTypes",
                table: "BagTypes");

            migrationBuilder.DropColumn(
                name: "ProductCost",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "BagTypes",
                newName: "BagType");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OrderId",
                table: "Transaction",
                newName: "IX_Transaction_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OrderId",
                table: "Product",
                newName: "IX_Product_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BagTypeId",
                table: "Product",
                newName: "IX_Product_BagTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Order",
                newName: "IX_Order_DeliveryAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Order",
                newName: "IX_Order_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BagType",
                table: "BagType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUserId",
                table: "Order",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryAddress_DeliveryAddressId",
                table: "Order",
                column: "DeliveryAddressId",
                principalTable: "DeliveryAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_BagType_BagTypeId",
                table: "Product",
                column: "BagTypeId",
                principalTable: "BagType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
