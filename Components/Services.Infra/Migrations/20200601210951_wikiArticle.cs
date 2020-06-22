using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Infra.Migrations
{
    public partial class wikiArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Access = table.Column<int>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

           

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "Access", "Description", "Text", "UserEmail" },
                values: new object[] { new Guid("21a7a844-f300-41bb-99f1-5b861c2d0190"), 1, "Article Description", "Article Text", "Email@Email.com" });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

           
        }
    }
}
