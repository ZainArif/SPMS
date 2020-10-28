using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Migrations
{
    public partial class Vendor_PurchaseDetail_SaleDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
