using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication_ColmanFactory1.Migrations
{
    public partial class removesizefeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
