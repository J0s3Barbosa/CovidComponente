using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class todoentityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("8a8056de-33b7-4970-b374-12fe91879267"));

            migrationBuilder.DeleteData(
                table: "VideoPlayer",
                keyColumn: "Id",
                keyValue: new Guid("fa7272b6-d64b-41c4-a99c-b9e758ee301c"));

            migrationBuilder.AddColumn<string>(
                name: "TotalTimeLapsed",
                table: "Todo",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 16, 21, 19, 17, 120, DateTimeKind.Local).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 16, 21, 19, 17, 120, DateTimeKind.Local).AddTicks(7108));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 16, 21, 19, 17, 119, DateTimeKind.Local).AddTicks(8538));

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Description", "Details", "TaskComplete", "TimeFinished", "TimeStarted", "TotalTimeLapsed", "UserEmail" },
                values: new object[] { new Guid("644276de-5007-41e6-9457-343fa1521855"), "Description", "Details", false, "5/16/2020 11:19:17 PM", "5/16/2020 9:19:17 PM", null, "Email@Email.com" });

            migrationBuilder.InsertData(
                table: "VideoPlayer",
                columns: new[] { "Id", "Description", "Link", "UserEmail" },
                values: new object[] { new Guid("e404fe71-ddee-4173-9b8d-83bd444fafa1"), "Description", "https://www.youtube.com/watch?v=4MkuId9X-hk", "Email@Email.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("644276de-5007-41e6-9457-343fa1521855"));

            migrationBuilder.DeleteData(
                table: "VideoPlayer",
                keyColumn: "Id",
                keyValue: new Guid("e404fe71-ddee-4173-9b8d-83bd444fafa1"));

            migrationBuilder.DropColumn(
                name: "TotalTimeLapsed",
                table: "Todo");

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
    }
}
