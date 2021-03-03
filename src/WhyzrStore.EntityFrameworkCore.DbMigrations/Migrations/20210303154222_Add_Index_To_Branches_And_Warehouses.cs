using Microsoft.EntityFrameworkCore.Migrations;

namespace WhyzrStore.Migrations
{
    public partial class Add_Index_To_Branches_And_Warehouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppWarehouses_Name",
                table: "AppWarehouses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppBranches_Name",
                table: "AppBranches",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppWarehouses_Name",
                table: "AppWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_AppBranches_Name",
                table: "AppBranches");
        }
    }
}
