using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pandas",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Awards = table.Column<List<string>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pandas", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pandas");
        }
    }
}
