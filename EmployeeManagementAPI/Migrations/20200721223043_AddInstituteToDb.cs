using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementAPI.Migrations
{
    public partial class AddInstituteToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    District = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Established = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Institutes");
        }
    }
}
