using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class userToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HealthCheck",
                keyColumn: "Id",
                keyValue: new Guid("541db5fd-f846-40b6-bdfe-4891817da0e5"));

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("e34ee25a-db9f-4152-aa1e-a37a7cda5f4b"));

            migrationBuilder.DeleteData(
                table: "VideoPlayer",
                keyColumn: "Id",
                keyValue: new Guid("c7e3c817-1171-489a-a468-cf40af29add2"));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "User",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 22, 1, 56, 2, 746, DateTimeKind.Local).AddTicks(5520));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 22, 1, 56, 2, 746, DateTimeKind.Local).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 22, 1, 56, 2, 745, DateTimeKind.Local).AddTicks(4859));

            migrationBuilder.InsertData(
                table: "HealthCheck",
                columns: new[] { "Id", "AppName", "TimeAccessed" },
                values: new object[] { new Guid("dd31b634-4d09-4634-be26-78daee23f160"), "API", "5/22/2020 1:56:02 AM" });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Description", "Details", "TaskComplete", "TimeFinished", "TimeStarted", "TotalTimeLapsed", "UserEmail" },
                values: new object[] { new Guid("7bf72486-bd35-4213-9287-a25168421c49"), "Description", "Details", false, "5/22/2020 3:56:02 AM", "5/22/2020 1:56:02 AM", null, "Email@Email.com" });

            migrationBuilder.InsertData(
                table: "VideoPlayer",
                columns: new[] { "Id", "Description", "Link", "UserEmail" },
                values: new object[] { new Guid("d5bb1ba4-7742-4930-a2fe-b03a3959a128"), "Description", "https://www.youtube.com/watch?v=4MkuId9X-hk", "Email@Email.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HealthCheck",
                keyColumn: "Id",
                keyValue: new Guid("dd31b634-4d09-4634-be26-78daee23f160"));

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: new Guid("7bf72486-bd35-4213-9287-a25168421c49"));

            migrationBuilder.DeleteData(
                table: "VideoPlayer",
                keyColumn: "Id",
                keyValue: new Guid("d5bb1ba4-7742-4930-a2fe-b03a3959a128"));

            migrationBuilder.DropColumn(
                name: "Token",
                table: "User");

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
    }
}
