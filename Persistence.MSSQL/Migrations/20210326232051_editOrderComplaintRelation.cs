using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class editOrderComplaintRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderComplaint",
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
                    OrderId = table.Column<string>(nullable: true),
                    AdClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderComplaint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderComplaint_AdClient_AdClientId",
                        column: x => x.AdClientId,
                        principalTable: "AdClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderComplaint_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderComplaint_AdIntervals_OrderId",
                        column: x => x.OrderId,
                        principalTable: "AdIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderComplaint_AdClientId",
                table: "OrderComplaint",
                column: "AdClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderComplaint_ClientId",
                table: "OrderComplaint",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderComplaint_OrderId",
                table: "OrderComplaint",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderComplaint");
        }
    }
}
