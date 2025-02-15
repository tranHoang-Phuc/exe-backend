using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuiToot.Server.Infrastructure.EfCore.Migrations
{
    public partial class AddPublicIdForBagType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvalibleProducts_BagTypes_BagTypeId",
                table: "AvalibleProducts");

            migrationBuilder.DropIndex(
                name: "IX_AvalibleProducts_BagTypeId",
                table: "AvalibleProducts");

            migrationBuilder.DropColumn(
                name: "BagTypeId",
                table: "AvalibleProducts");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "BagTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AvalibleProducts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "BagTypes",
                columns: new[] { "Id", "Description", "Name", "Price", "PublicId", "UnitsInStock", "Url" },
                values: new object[] { "d38ad7f0-bac6-462c-8cf8-c7a424c19992", "Túi hình vuông", "Tote  Vuông", 0m, null, 20, "https://res.cloudinary.com/dbrm5eowo/image/upload/v1737516248/totebag-light-new_large_gm07d2.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BagTypes",
                keyColumn: "Id",
                keyValue: "d38ad7f0-bac6-462c-8cf8-c7a424c19992");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "BagTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AvalibleProducts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "BagTypeId",
                table: "AvalibleProducts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvalibleProducts_BagTypeId",
                table: "AvalibleProducts",
                column: "BagTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvalibleProducts_BagTypes_BagTypeId",
                table: "AvalibleProducts",
                column: "BagTypeId",
                principalTable: "BagTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
