using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwaggerTest.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationsModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MicrocontrollerID = table.Column<int>(type: "int", nullable: false),
                    NotificationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationsModel");

            migrationBuilder.DropTable(
                name: "ValuesModels");
        }
    }
}
