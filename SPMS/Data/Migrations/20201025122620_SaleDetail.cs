using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class SaleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleDetail",
                columns: table => new
                {
                    Sale_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purchase_Id = table.Column<int>(nullable: false),
                    Sale_Date = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit_Cost = table.Column<int>(nullable: false),
                    Total_Cost = table.Column<int>(nullable: false),
                    Amount_Received = table.Column<int>(nullable: false),
                    Amount_Balance = table.Column<int>(nullable: false),
                    Expense = table.Column<int>(nullable: false),
                    Entry_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail", x => x.Sale_Id);
                    table.ForeignKey(
                        name: "FK_SaleDetail_PurchaseDetail_Purchase_Id",
                        column: x => x.Purchase_Id,
                        principalTable: "PurchaseDetail",
                        principalColumn: "Purchase_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_Purchase_Id",
                table: "SaleDetail",
                column: "Purchase_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleDetail");
        }
    }
}
