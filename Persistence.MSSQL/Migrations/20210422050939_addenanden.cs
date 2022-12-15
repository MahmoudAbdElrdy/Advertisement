using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.MSSQL.Migrations
{
    public partial class addenanden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "BodyAr",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyEn",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectAr",
                table: "Notification",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectEn",
                table: "Notification",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyAr",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "BodyEn",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SubjectAr",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SubjectEn",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Notification",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
