using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class addAdComplaintTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdComplaints",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ComplaintReason = table.Column<string>(nullable: true),
                    ComplaintReasonReplay = table.Column<string>(nullable: true),
                    IsComplaintSeen = table.Column<bool>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    AdId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdComplaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdComplaints_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdComplaints_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdComplaints_AdId",
                table: "AdComplaints",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdComplaints_ClientId",
                table: "AdComplaints",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdComplaints");
        }
    }
}
