using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGarageParking.Migrations
{
    /// <inheritdoc />
    public partial class ownerNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "VehicleInGarages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "VehicleInGarages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
