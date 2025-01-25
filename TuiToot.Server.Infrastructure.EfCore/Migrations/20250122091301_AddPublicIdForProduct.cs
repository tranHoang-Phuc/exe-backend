using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuiToot.Server.Infrastructure.EfCore.Migrations
{
    public partial class AddPublicIdForProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicdId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BagType",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitsInStock",
                table: "BagType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicdId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BagType");

            migrationBuilder.DropColumn(
                name: "UnitsInStock",
                table: "BagType");
        }
    }
}
