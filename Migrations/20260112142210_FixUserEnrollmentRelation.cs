using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    /// <inheritdoc />
    public partial class FixUserEnrollmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_ApplicationUserId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_ApplicationUserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Enrollments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Enrollments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ApplicationUserId",
                table: "Enrollments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_ApplicationUserId",
                table: "Enrollments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
