using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Frank_Workshop.Migrations
{
    public partial class UpdateRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Author",
                table: "Recipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Recipe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastUpdated",
                table: "Recipe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Recipe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Recipe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Recipe",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "DateLastUpdated",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Recipe");
        }
    }
}
