using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class videoPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("d930128c-f9ce-48c5-8a0b-17c1e63ace42"));

            migrationBuilder.CreateTable(
                name: "VideoPlayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPlayer", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 23, 1, 51, 50, DateTimeKind.Local).AddTicks(6866));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 23, 1, 51, 50, DateTimeKind.Local).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 23, 1, 51, 44, DateTimeKind.Local).AddTicks(767));

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Description", "Details", "TaskComplete", "TimeFinished", "TimeStarted", "UserEmail" },
                values: new object[] { new Guid("8a8056de-33b7-4970-b374-12fe91879267"), "Description", "Details", false, "5/14/2020 1:01:51 AM", "5/13/2020 11:01:51 PM", "Email@Email.com" });

            migrationBuilder.InsertData(
                table: "VideoPlayer",
                columns: new[] { "Id", "Description", "Link", "UserEmail" },
                values: new object[] { new Guid("fa7272b6-d64b-41c4-a99c-b9e758ee301c"), "Description", "https://www.youtube.com/watch?v=4MkuId9X-hk", "Email@Email.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoPlayer");

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("8a8056de-33b7-4970-b374-12fe91879267"));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 20, 18, 57, 938, DateTimeKind.Local).AddTicks(1213));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 20, 18, 57, 938, DateTimeKind.Local).AddTicks(1352));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 20, 18, 57, 931, DateTimeKind.Local).AddTicks(1687));

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Description", "Details", "TaskComplete", "TimeFinished", "TimeStarted", "UserEmail" },
                values: new object[] { new Guid("d930128c-f9ce-48c5-8a0b-17c1e63ace42"), "Description", "Details", false, "5/13/2020 10:18:57 PM", "5/13/2020 8:18:57 PM", "Email@Email.com" });
        }
    }
}
