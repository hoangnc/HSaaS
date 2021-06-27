using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagement.Migrations
{
    public partial class TestComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContentChange",
                table: "DocumentManagementDocuments",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Content change",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContentChange",
                table: "DocumentManagementDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Content change");
        }
    }
}
