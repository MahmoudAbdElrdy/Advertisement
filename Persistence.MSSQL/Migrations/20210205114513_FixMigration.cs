using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Image",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                table: "Image",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SpaceImage_Rejected",
                table: "Image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpaceInfoId",
                table: "Image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCodes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    Code = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ServiceTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpaceInfos",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Rejected = table.Column<bool>(nullable: false),
                    IsAuction = table.Column<bool>(nullable: false),
                    CityId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceInfos_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpaceInfos_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    duration = table.Column<int>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    AdType = table.Column<int>(nullable: false),
                    AdCategory = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SpaceInfoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_SpaceInfos_SpaceInfoId",
                        column: x => x.SpaceInfoId,
                        principalTable: "SpaceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Lng = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    SpaceInfoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_SpaceInfos_SpaceInfoId",
                        column: x => x.SpaceInfoId,
                        principalTable: "SpaceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdClient",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    AdId = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdClient_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdClient_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdStatus",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    AdId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdStatus_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    AdId = table.Column<string>(nullable: true),
                    SeriousSubscriptionAmount = table.Column<decimal>(nullable: true),
                    AuctionStatus = table.Column<int>(nullable: false),
                    AuctionDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auctions_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FreeServices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    AdId = table.Column<string>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeServices_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FreeServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaidServices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    AdId = table.Column<string>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaidServices_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaidServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    RefNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AdId = table.Column<string>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true),
                    TranType = table.Column<int>(nullable: false),
                    Dir = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    AdId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    AdClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_AdClient_AdClientId",
                        column: x => x.AdClientId,
                        principalTable: "AdClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Installments_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuctionSubiscriber",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    AuctionId = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    AuctionStatus = table.Column<int>(nullable: false),
                    AuctionDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionSubiscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionSubiscriber_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionSubiscriber_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ClientId",
                table: "Image",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_SpaceInfoId",
                table: "Image",
                column: "SpaceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AdClient_AdId",
                table: "AdClient",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdClient_ClientId",
                table: "AdClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_SpaceInfoId",
                table: "Ads",
                column: "SpaceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AdStatus_AdId",
                table: "AdStatus",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_AdId",
                table: "Auctions",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionSubiscriber_AuctionId",
                table: "AuctionSubiscriber",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionSubiscriber_ClientId",
                table: "AuctionSubiscriber",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FreeServices_AdId",
                table: "FreeServices",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_FreeServices_ServiceId",
                table: "FreeServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_AdClientId",
                table: "Installments",
                column: "AdClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_AdId",
                table: "Installments",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_SpaceInfoId",
                table: "Location",
                column: "SpaceInfoId",
                unique: true,
                filter: "[SpaceInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaidServices_AdId",
                table: "PaidServices",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidServices_ServiceId",
                table: "PaidServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceInfos_CityId",
                table: "SpaceInfos",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceInfos_ClientId",
                table: "SpaceInfos",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AdId",
                table: "Transactions",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCodes_UserId",
                table: "UserCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_AspNetUsers_ClientId",
                table: "Image",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_SpaceInfos_SpaceInfoId",
                table: "Image",
                column: "SpaceInfoId",
                principalTable: "SpaceInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_AspNetUsers_ClientId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_SpaceInfos_SpaceInfoId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "AdStatus");

            migrationBuilder.DropTable(
                name: "AuctionSubiscriber");

            migrationBuilder.DropTable(
                name: "FreeServices");

            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "PaidServices");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserCodes");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "AdClient");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "SpaceInfos");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Image_ClientId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_SpaceInfoId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "SpaceImage_Rejected",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "SpaceInfoId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
