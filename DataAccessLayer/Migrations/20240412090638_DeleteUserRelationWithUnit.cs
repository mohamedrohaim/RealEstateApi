using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class DeleteUserRelationWithUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_units_AspNetUsers_UserId",
                table: "units");

            migrationBuilder.DropIndex(
                name: "IX_units_UserId",
                table: "units");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "units");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "units",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_units_UserId",
                table: "units",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_units_AspNetUsers_UserId",
                table: "units",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
