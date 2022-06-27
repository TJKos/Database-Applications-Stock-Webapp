using Microsoft.EntityFrameworkCore.Migrations;

namespace APBDProjekt.Server.Data.Migrations
{
    public partial class Podmianka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Primary_Exchange",
                table: "StockInfo",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Primary_Exchange",
                table: "StockInfo");
        }
    }
}
