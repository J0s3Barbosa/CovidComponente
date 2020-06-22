using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class todo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    TimeStarted = table.Column<string>(nullable: true),
                    TimeFinished = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: false),
                    TaskComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 4, 50, 45, 0, DateTimeKind.Local).AddTicks(3256));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 4, 50, 45, 0, DateTimeKind.Local).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 4, 50, 44, 992, DateTimeKind.Local).AddTicks(4241));
        }
    }
}
