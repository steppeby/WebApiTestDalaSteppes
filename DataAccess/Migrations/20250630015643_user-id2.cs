using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userid2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Weightings");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToUserId",
                table: "Weightings",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weightings_AssignedToUserId",
                table: "Weightings",
                column: "AssignedToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weightings_AspNetUsers_AssignedToUserId",
                table: "Weightings",
                column: "AssignedToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weightings_AspNetUsers_AssignedToUserId",
                table: "Weightings");

            migrationBuilder.DropIndex(
                name: "IX_Weightings_AssignedToUserId",
                table: "Weightings");

            migrationBuilder.DropColumn(
                name: "AssignedToUserId",
                table: "Weightings");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Weightings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
