using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class FreeService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceTypeId",
                table: "FreeServices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreeServices_ServiceTypeId",
                table: "FreeServices",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FreeServices_ServiceTypes_ServiceTypeId",
                table: "FreeServices",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreeServices_ServiceTypes_ServiceTypeId",
                table: "FreeServices");

            migrationBuilder.DropIndex(
                name: "IX_FreeServices_ServiceTypeId",
                table: "FreeServices");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "FreeServices");
        }
    }
}
