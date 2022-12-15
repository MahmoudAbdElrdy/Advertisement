using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class addrelationbetweenpaidservicesandadinterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaidServices_Ads_AdId",
                table: "PaidServices");

            migrationBuilder.DropIndex(
                name: "IX_PaidServices_AdId",
                table: "PaidServices");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "PaidServices");

            migrationBuilder.AddColumn<string>(
                name: "AdIntervalId",
                table: "PaidServices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaidServices_AdIntervalId",
                table: "PaidServices",
                column: "AdIntervalId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaidServices_AdIntervals_AdIntervalId",
                table: "PaidServices",
                column: "AdIntervalId",
                principalTable: "AdIntervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaidServices_AdIntervals_AdIntervalId",
                table: "PaidServices");

            migrationBuilder.DropIndex(
                name: "IX_PaidServices_AdIntervalId",
                table: "PaidServices");

            migrationBuilder.DropColumn(
                name: "AdIntervalId",
                table: "PaidServices");

            migrationBuilder.AddColumn<string>(
                name: "AdId",
                table: "PaidServices",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaidServices_AdId",
                table: "PaidServices",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaidServices_Ads_AdId",
                table: "PaidServices",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
