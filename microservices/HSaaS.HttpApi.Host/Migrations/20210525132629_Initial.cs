using Microsoft.EntityFrameworkCore.Migrations;

namespace HSaaS.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterDataCompanies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDataCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterDataDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDataDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterDataDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDataDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterDataModules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDataModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterDataUserDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDataUserDepartments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataCompanies_Name",
                table: "MasterDataCompanies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataDepartments_Name",
                table: "MasterDataDepartments",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataDocumentTypes_Name",
                table: "MasterDataDocumentTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataModules_Name",
                table: "MasterDataModules",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataUserDepartments_UserName_DepartmentCode",
                table: "MasterDataUserDepartments",
                columns: new[] { "UserName", "DepartmentCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterDataCompanies");

            migrationBuilder.DropTable(
                name: "MasterDataDepartments");

            migrationBuilder.DropTable(
                name: "MasterDataDocumentTypes");

            migrationBuilder.DropTable(
                name: "MasterDataModules");

            migrationBuilder.DropTable(
                name: "MasterDataUserDepartments");
        }
    }
}
