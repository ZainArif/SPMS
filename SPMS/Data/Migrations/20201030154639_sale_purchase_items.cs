using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class sale_purchase_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    Purchase_Item_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purchase_Id = table.Column<int>(nullable: false),
                    Item_Id = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Available_Quantity = table.Column<int>(nullable: false),
                    Unit_Cost = table.Column<int>(nullable: false),
                    Total_Cost = table.Column<int>(nullable: false),
                    Entry_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.Purchase_Item_Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Item_Item_Id",
                        column: x => x.Item_Id,
                        principalTable: "Item",
                        principalColumn: "Item_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_PurchaseDetail_Purchase_Id",
                        column: x => x.Purchase_Id,
                        principalTable: "PurchaseDetail",
                        principalColumn: "Purchase_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Sale_Item_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale_Id = table.Column<int>(nullable: false),
                    Purchase_Item_Id = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit_Price = table.Column<int>(nullable: false),
                    Total_Price = table.Column<int>(nullable: false),
                    Entry_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Sale_Item_Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_PurchaseItems_Purchase_Item_Id",
                        column: x => x.Purchase_Item_Id,
                        principalTable: "PurchaseItems",
                        principalColumn: "Purchase_Item_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_SaleDetail_Sale_Id",
                        column: x => x.Sale_Id,
                        principalTable: "SaleDetail",
                        principalColumn: "Sale_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_Item_Id",
                table: "PurchaseItems",
                column: "Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_Purchase_Id",
                table: "PurchaseItems",
                column: "Purchase_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_Purchase_Item_Id",
                table: "SaleItems",
                column: "Purchase_Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_Sale_Id",
                table: "SaleItems",
                column: "Sale_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "PurchaseItems");
        }
    }
}
