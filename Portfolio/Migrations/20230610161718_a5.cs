using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class a5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "logs",
                newName: "Uemail");

            migrationBuilder.RenameColumn(
                name: "Mode_Id",
                table: "logs",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Mode",
                table: "logs",
                newName: "Time_Left_To_Mature");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "logs",
                newName: "lastUpdate");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "logs",
                newName: "Maturity_Amount");

            migrationBuilder.AddColumn<string>(
                name: "Bank_Name",
                table: "logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Investment_Amount",
                table: "logs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Investment_Start_Date",
                table: "logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Maturity_Date",
                table: "logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ROI",
                table: "logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank_Name",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "Investment_Amount",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "Investment_Start_Date",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "Maturity_Date",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "ROI",
                table: "logs");

            migrationBuilder.RenameColumn(
                name: "lastUpdate",
                table: "logs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Uemail",
                table: "logs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "logs",
                newName: "Mode_Id");

            migrationBuilder.RenameColumn(
                name: "Time_Left_To_Mature",
                table: "logs",
                newName: "Mode");

            migrationBuilder.RenameColumn(
                name: "Maturity_Amount",
                table: "logs",
                newName: "Amount");
        }
    }
}
