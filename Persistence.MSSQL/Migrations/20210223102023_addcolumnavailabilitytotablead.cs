using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class addcolumnavailabilitytotablead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Availability",
                table: "Ads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Ads");
        }
    }
}
