using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGarageParking.Migrations
{
    /// <inheritdoc />
    public partial class LastEntryTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEntryTime",
                table: "Garages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEntryTime",
                table: "Garages");
        }
    }
}
