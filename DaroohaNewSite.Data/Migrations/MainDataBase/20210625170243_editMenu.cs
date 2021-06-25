using Microsoft.EntityFrameworkCore.Migrations;

namespace DaroohaNewSite.Data.Migrations.MainDataBase
{
    public partial class editMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "subMenuName",
                table: "Tbl_Menues",
                newName: "MenuName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuName",
                table: "Tbl_Menues",
                newName: "subMenuName");
        }
    }
}
