using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentManagementDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drafter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppliedToEntire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedToEntire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplaceFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplaceEffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelateToDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDCAudited = table.Column<bool>(type: "bit", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    FormType = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IssuedStatusId = table.Column<int>(type: "int", nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentManagementDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentManagementAppendixes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppendixNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drafter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auditor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppliedToEntire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedToEntire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplaceFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplaceEffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelateToDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDCAudited = table.Column<bool>(type: "bit", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    FormType = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IssuedStatusId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentManagementAppendixes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentManagementAppendixes_DocumentManagementDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DocumentManagementDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentManagementAppendixes_DocumentId",
                table: "DocumentManagementAppendixes",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentManagementAppendixes_Name_CompanyName_DepartmentName",
                table: "DocumentManagementAppendixes",
                columns: new[] { "Name", "CompanyName", "DepartmentName" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentManagementDocuments_Name_CompanyName_DepartmentName",
                table: "DocumentManagementDocuments",
                columns: new[] { "Name", "CompanyName", "DepartmentName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentManagementAppendixes");

            migrationBuilder.DropTable(
                name: "DocumentManagementDocuments");
        }
    }
}
