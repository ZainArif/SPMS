using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Customer_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Name = table.Column<string>(nullable: false),
                    Customer_No = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Customer_Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Item_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(nullable: false),
                    Item_Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Item_Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Vender_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vender_Name = table.Column<string>(nullable: false),
                    Contact_Person_Name = table.Column<string>(nullable: false),
                    Contect_Person_No = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Vender_Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetail",
                columns: table => new
                {
                    Sale_Id = table.Column<int>(nullable: false),
                    Customer_Id = table.Column<int>(nullable: false),
                    Sale_Date = table.Column<DateTime>(nullable: false),
                    Amount_Received = table.Column<int>(nullable: false),
                    Amount_Balance = table.Column<int>(nullable: false),
                    Entry_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail", x => x.Sale_Id);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Customer_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customer",
                        principalColumn: "Customer_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    Purchase_Id = table.Column<int>(nullable: false),
                    Vender_Id = table.Column<int>(nullable: false),
                    Purchase_Date = table.Column<DateTime>(nullable: false),
                    Purchase_Description = table.Column<string>(nullable: true),
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
                name: "IX_PurchaseDetail_Vender_Id",
                table: "PurchaseDetail",
                column: "Vender_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_Item_Id",
                table: "PurchaseItems",
                column: "Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_Purchase_Id",
                table: "PurchaseItems",
                column: "Purchase_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_Customer_Id",
                table: "SaleDetail",
                column: "Customer_Id");

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

            migrationBuilder.DropTable(
                name: "SaleDetail");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
