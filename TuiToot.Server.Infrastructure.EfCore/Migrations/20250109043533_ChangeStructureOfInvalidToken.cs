using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuiToot.Server.Infrastructure.EfCore.Migrations
{
    public partial class ChangeStructureOfInvalidToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvalidTokens",
                table: "InvalidTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "InvalidTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InvalidTokens",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvalidTokens",
                table: "InvalidTokens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvalidTokens",
                table: "InvalidTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InvalidTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "InvalidTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvalidTokens",
                table: "InvalidTokens",
                column: "Token");
        }
    }
}
