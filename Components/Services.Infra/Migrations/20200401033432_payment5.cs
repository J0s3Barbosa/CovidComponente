using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Services.Infra.Migrations
{
    public partial class payment5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaidDate",
                table: "Payment",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DueDate",
                table: "Payment",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");



            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                columns: new[] { "BarCode", "DueDate", "PaidDate" },
                values: new object[] { "0000 1111 2222 3333 4444", "05/04/2020", "05/04/2020" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PaidDate",
                table: "Payment",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Payment",
                type: "date",
                nullable: false,
                oldClrType: typeof(string));



            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                columns: new[] { "BarCode", "DueDate", "PaidDate" },
                values: new object[] { "1", new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 31, 23, 58, 46, 367, DateTimeKind.Local).AddTicks(147) });
        }
    }
}
