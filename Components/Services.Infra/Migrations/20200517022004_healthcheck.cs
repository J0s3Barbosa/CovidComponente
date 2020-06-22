using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class healthcheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("644276de-5007-41e6-9457-343fa1521855"));

            migrationBuilder.DeleteData(
                table: "VideoPlayer",
                keyColumn: "Id",
                keyValue: new Guid("e404fe71-ddee-4173-9b8d-83bd444fafa1"));

            migrationBuilder.CreateTable(
                name: "HealthCheck",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeAccessed = table.Column<string>(nullable: false),
                    AppName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCheck", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 16, 23, 20, 2, 882, DateTimeKind.Local).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 16, 23, 20, 2, 882, DateTimeKind.Local).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 16, 23, 20, 2, 881, DateTimeKind.Local).AddTicks(8484));

            migrationBuilder.InsertData(
                table: "HealthCheck",
                columns: new[] { "Id", "AppName", "TimeAccessed" },
                values: new object[] { new Guid("541db5fd-f846-40b6-bdfe-4891817da0e5"), "API", "5/16/2020 11:20:02 PM" });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Description", "Details", "TaskComplete", "TimeFinished", "TimeStarted", "TotalTimeLapsed", "UserEmail" },
                values: new object[] { new Guid("e34ee25a-db9f-4152-aa1e-a37a7cda5f4b"), "Description", "Details", false, "5/17/2020 1:20:02 AM", "5/16/2020 11:20:02 PM", null, "Email@Email.com" });

            migrationBuilder.InsertData(
                table: "VideoPlayer",
                columns: new[] { "Id", "Description", "Link", "UserEmail" },
                values: new object[] { new Guid("c7e3c817-1171-489a-a468-cf40af29add2"), "Description", "https://www.youtube.com/watch?v=4MkuId9X-hk", "Email@Email.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthCheck");

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("e34ee25a-db9f-4152-aa1e-a37a7cda5f4b"));

            migrationBuilder.DeleteData(
                table: "VideoPlayer",
                keyColumn: "Id",
                keyValue: new Guid("c7e3c817-1171-489a-a468-cf40af29add2"));

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
    }
}
