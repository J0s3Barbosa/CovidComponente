using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class usernumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "User",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                column: "Phone",
                value: "99999999999");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 1, 44, 21, 715, DateTimeKind.Local).AddTicks(8212));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 1, 44, 21, 715, DateTimeKind.Local).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 5, 13, 1, 44, 21, 708, DateTimeKind.Local).AddTicks(7451));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                column: "Phone",
                value: 0);
        }
    }
}
