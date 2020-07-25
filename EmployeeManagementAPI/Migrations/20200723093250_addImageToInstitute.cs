using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementAPI.Migrations
{
    public partial class addImageToInstitute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Institutes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Institutes");
        }
    }
}
