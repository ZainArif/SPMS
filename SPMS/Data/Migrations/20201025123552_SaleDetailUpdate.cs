using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class SaleDetailUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "SaleDetail",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_No",
                table: "SaleDetail",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Customer_No",
                table: "SaleDetail");
        }
    }
}
