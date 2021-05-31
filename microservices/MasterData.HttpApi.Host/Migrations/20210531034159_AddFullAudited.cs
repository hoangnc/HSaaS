using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterData.Migrations
{
    public partial class AddFullAudited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "MasterDataUserDepartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "MasterDataUserDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "MasterDataUserDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "MasterDataUserDepartments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MasterDataUserDepartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "MasterDataUserDepartments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "MasterDataUserDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "MasterDataModules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "MasterDataModules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "MasterDataModules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "MasterDataModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MasterDataModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "MasterDataModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "MasterDataModules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "MasterDataDocumentTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "MasterDataDocumentTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "MasterDataDocumentTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "MasterDataDocumentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MasterDataDocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "MasterDataDocumentTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "MasterDataDocumentTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "MasterDataDepartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "MasterDataDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "MasterDataDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "MasterDataDepartments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MasterDataDepartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "MasterDataDepartments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "MasterDataDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "MasterDataCompanies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "MasterDataCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "MasterDataCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "MasterDataCompanies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MasterDataCompanies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "MasterDataCompanies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "MasterDataCompanies",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "MasterDataUserDepartments");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "MasterDataModules");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "MasterDataDocumentTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "MasterDataDepartments");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "MasterDataCompanies");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "MasterDataCompanies");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "MasterDataCompanies");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "MasterDataCompanies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MasterDataCompanies");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "MasterDataCompanies");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "MasterDataCompanies");
        }
    }
}
