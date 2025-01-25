using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuiToot.Server.Infrastructure.EfCore.Migrations
{
    public partial class RemoveDeposite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositeAmount",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Transaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DepositeAmount",
                table: "Transaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Transaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
