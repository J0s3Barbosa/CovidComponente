using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Services.Infra.Migrations
{
    public partial class payment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
      name: "Payment");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    BarCode = table.Column<string>(nullable: true),
                    PaidDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.InsertData(
            table: "Payment",
            columns: new[] { "Id", "BarCode", "Description", "DueDate", "PaidDate" },
            values: new object[] { new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"), "1", "Rent", new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 31, 18, 5, 35, 799, DateTimeKind.Local).AddTicks(6973) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
    name: "Payment");
        }
    }
}
