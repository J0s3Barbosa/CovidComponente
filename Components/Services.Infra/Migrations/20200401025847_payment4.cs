using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Services.Infra.Migrations
{
    public partial class payment4 : Migration
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
                    Description = table
                    .Column<string>(type: "varchar(50)", nullable: false),
                    DueDate = table
                    .Column<DateTime>(type: "Date", nullable: false),
                    BarCode = table.Column<string>(nullable: true),
                    PaidDate = table
                    .Column<DateTime>(type: "Date", nullable: true)
                });

            migrationBuilder.InsertData(
          table: "Payment",
          columns: new[] { "Id", "BarCode", "Description", "DueDate", "PaidDate" },
          values: new object[]
          {
              new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"),
              "0000 1111 2222 3333 4444",
              "Rent",
          "05/04/2020",
              "05/04/2020"
          });



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
 name: "Payment");
        }
    }
}
