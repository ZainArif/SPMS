using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class PurchaseDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    Purchase_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vender_Id = table.Column<int>(nullable: false),
                    Purchase_Date = table.Column<DateTime>(nullable: false),
                    Purchase_Item = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit_Cost = table.Column<int>(nullable: false),
                    Total_Cost = table.Column<int>(nullable: false),
                    Amount_Paid = table.Column<int>(nullable: false),
                    Amount_Balance = table.Column<int>(nullable: false),
                    Entry_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.Purchase_Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_Vendor_Vender_Id",
                        column: x => x.Vender_Id,
                        principalTable: "Vendor",
                        principalColumn: "Vender_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_Vender_Id",
                table: "PurchaseDetail",
                column: "Vender_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetail");
        }
    }
}
