using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodConvo.Migrations
{
    public partial class inprogressbool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "inProgress",
                table: "Conversations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inProgress",
                table: "Conversations");
        }
    }
}
