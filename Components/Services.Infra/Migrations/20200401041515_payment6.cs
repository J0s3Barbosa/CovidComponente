using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class payment6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Payment",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 4, 1, 1, 15, 14, 765, DateTimeKind.Local).AddTicks(8713));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 4, 1, 1, 15, 14, 765, DateTimeKind.Local).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 4, 1, 1, 15, 14, 758, DateTimeKind.Local).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                columns: new[] { "Link", "Notes" },
                values: new object[] { "https://www.portalunsoft.com.br/area-do-cliente/safira/", "Notes" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Payment");

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186622"),
                column: "CreationDate",
                value: new DateTime(2020, 4, 1, 0, 34, 30, 778, DateTimeKind.Local).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("a714554f-f363-42f1-b41a-81ee85186661"),
                column: "CreationDate",
                value: new DateTime(2020, 4, 1, 0, 34, 30, 778, DateTimeKind.Local).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "AirPlane",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                column: "CreationDate",
                value: new DateTime(2020, 4, 1, 0, 34, 30, 769, DateTimeKind.Local).AddTicks(9409));
        }
    }
}
