using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class useradmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "User",
                nullable: false,
                defaultValue: false);

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "User");

           
        }
    }
}
