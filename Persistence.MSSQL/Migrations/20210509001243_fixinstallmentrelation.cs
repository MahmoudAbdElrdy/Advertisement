using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class fixinstallmentrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropIndex(
        //        name: "IX_Installments_AdId",
        //        table: "Installments");

        //    migrationBuilder.DropColumn(
        //        name: "AdId",
        //        table: "Installments");

        //    migrationBuilder.AddColumn<string>(
        //        name: "AdIntervalId",
        //        table: "Installments",
        //        nullable: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Installments_AdIntervalId",
        //        table: "Installments",
        //        column: "AdIntervalId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_Installments_AdIntervals_AdIntervalId",
        //        table: "Installments",
        //        column: "AdIntervalId",
        //        principalTable: "AdIntervals",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Restrict);
        }

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_Installments_AdIntervals_AdIntervalId",
        //        table: "Installments");

        //    migrationBuilder.DropIndex(
        //        name: "IX_Installments_AdIntervalId",
        //        table: "Installments");

        //    migrationBuilder.DropColumn(
        //        name: "AdIntervalId",
        //        table: "Installments");

        //    migrationBuilder.AddColumn<string>(
        //        name: "AdId",
        //        table: "Installments",
        //        type: "nvarchar(256)",
        //        nullable: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Installments_AdId",
        //        table: "Installments",
        //        column: "AdId");
        //}
    }
}
