using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuiToot.Server.Infrastructure.EfCore.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "TransactionPayment");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OrderId",
                table: "TransactionPayment",
                newName: "IX_TransactionPayment_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionPayment",
                table: "TransactionPayment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionPayment_Orders_OrderId",
                table: "TransactionPayment",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionPayment_Orders_OrderId",
                table: "TransactionPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionPayment",
                table: "TransactionPayment");

            migrationBuilder.RenameTable(
                name: "TransactionPayment",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionPayment_OrderId",
                table: "Transactions",
                newName: "IX_Transactions_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                table: "Transactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
