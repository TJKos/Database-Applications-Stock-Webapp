using Microsoft.EntityFrameworkCore.Migrations;

namespace APBDProjekt.Server.Data.Migrations
{
    public partial class Asocjacyjnauserstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "StockInfo");

            migrationBuilder.AddColumn<string>(
                name: "Market",
                table: "StockInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockInfo_ApplicationUser",
                columns: table => new
                {
                    IdStockInfo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUser = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInfo_ApplicationUser", x => new { x.IdStockInfo, x.IdUser });
                    table.ForeignKey(
                        name: "FK_StockInfo_ApplicationUser_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInfo_ApplicationUser_StockInfo_IdStockInfo",
                        column: x => x.IdStockInfo,
                        principalTable: "StockInfo",
                        principalColumn: "IdStockInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_ApplicationUser_IdUser",
                table: "StockInfo_ApplicationUser",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockInfo_ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Market",
                table: "StockInfo");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "StockInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
