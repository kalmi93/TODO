using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.DAL.Migrations
{
    public partial class addedstatusfortask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskStatus",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "Tasks");
        }
    }
}
