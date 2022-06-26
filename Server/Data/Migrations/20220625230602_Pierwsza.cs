using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APBDProjekt.Server.Data.Migrations
{
    public partial class Pierwsza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    IdArticle = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Published_Utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Article_Url = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Favicon_Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.IdArticle);
                });

            migrationBuilder.CreateTable(
                name: "StockInfo",
                columns: table => new
                {
                    IdStockInfo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Ticker = table.Column<string>(type: "TEXT", nullable: false),
                    Locale = table.Column<string>(type: "TEXT", nullable: false),
                    Phone_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Homepage_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Sic_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Logo_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Icon_Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInfo", x => x.IdStockInfo);
                    table.ForeignKey(
                        name: "FK_StockInfo_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockChartData",
                columns: table => new
                {
                    IdStockChartData = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdStockInfo = table.Column<int>(type: "INTEGER", nullable: false),
                    v = table.Column<long>(type: "INTEGER", nullable: false),
                    vw = table.Column<double>(type: "REAL", nullable: false),
                    o = table.Column<double>(type: "REAL", nullable: false),
                    c = table.Column<double>(type: "REAL", nullable: false),
                    h = table.Column<double>(type: "REAL", nullable: false),
                    l = table.Column<double>(type: "REAL", nullable: false),
                    t = table.Column<long>(type: "INTEGER", nullable: false),
                    n = table.Column<long>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockChartData", x => x.IdStockChartData);
                    table.ForeignKey(
                        name: "FK_StockChartData_StockInfo_IdStockInfo",
                        column: x => x.IdStockInfo,
                        principalTable: "StockInfo",
                        principalColumn: "IdStockInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockInfo_Article",
                columns: table => new
                {
                    IdStockInfo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdArticle = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInfo_Article", x => new { x.IdStockInfo, x.IdArticle });
                    table.ForeignKey(
                        name: "FK_StockInfo_Article_Article_IdArticle",
                        column: x => x.IdArticle,
                        principalTable: "Article",
                        principalColumn: "IdArticle",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInfo_Article_StockInfo_IdStockInfo",
                        column: x => x.IdStockInfo,
                        principalTable: "StockInfo",
                        principalColumn: "IdStockInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockChartData_IdStockInfo",
                table: "StockChartData",
                column: "IdStockInfo");

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_ApplicationUserId",
                table: "StockInfo",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_Article_IdArticle",
                table: "StockInfo_Article",
                column: "IdArticle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockChartData");

            migrationBuilder.DropTable(
                name: "StockInfo_Article");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "StockInfo");
        }
    }
}
