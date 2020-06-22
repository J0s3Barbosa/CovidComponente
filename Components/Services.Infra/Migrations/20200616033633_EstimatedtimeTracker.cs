using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class EstimatedtimeTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "EstimatedtimeTracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Activity = table.Column<string>(nullable: false),
                    TimeStarted = table.Column<string>(nullable: true),
                    TimeEnded = table.Column<string>(nullable: true),
                    TimeSpent = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimatedtimeTracker", x => x.Id);
                });

           

            migrationBuilder.InsertData(
                table: "EstimatedtimeTracker",
                columns: new[] { "Id", "Activity", "Date", "Owner", "Project", "TimeEnded", "TimeSpent", "TimeStarted" },
                values: new object[] { new Guid("f9cb274a-e3f9-42c0-88ff-f50c4eecb01f"), "Activity", "6/16/2020 12:36:31 AM", "Email@Email.com", "Project", "02:36:31", "00:36:31", "00:36:31" });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstimatedtimeTracker");

           
        }
    }
}
