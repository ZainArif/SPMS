using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class customer_item_purchase_sale_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_PurchaseDetail_Purchase_Id",
                table: "SaleDetail");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetail_Purchase_Id",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Customer_No",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Purchase_Id",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Total_Cost",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Unit_Cost",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Purchase_Item",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "Total_Cost",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "Unit_Cost",
                table: "PurchaseDetail");

            migrationBuilder.AddColumn<int>(
                name: "Customer_Id",
                table: "SaleDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_Description",
                table: "PurchaseDetail",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_Customer_Id",
                table: "SaleDetail",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Customer_Customer_Id",
                table: "SaleDetail",
                column: "Customer_Id",
                principalTable: "Customer",
                principalColumn: "Customer_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Customer_Customer_Id",
                table: "SaleDetail");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetail_Customer_Id",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Customer_Id",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Purchase_Description",
                table: "PurchaseDetail");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "SaleDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_No",
                table: "SaleDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Purchase_Id",
                table: "SaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total_Cost",
                table: "SaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Unit_Cost",
                table: "SaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_Item",
                table: "PurchaseDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchaseDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total_Cost",
                table: "PurchaseDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Unit_Cost",
                table: "PurchaseDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_Purchase_Id",
                table: "SaleDetail",
                column: "Purchase_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_PurchaseDetail_Purchase_Id",
                table: "SaleDetail",
                column: "Purchase_Id",
                principalTable: "PurchaseDetail",
                principalColumn: "Purchase_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
