using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_GymClasses_GymClassId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "GymClasses");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "GymClassId",
                table: "Enrollments",
                newName: "MembershipId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_GymClassId",
                table: "Enrollments",
                newName: "IX_Enrollments_MembershipId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Memberships",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Enrollments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasedAt",
                table: "Enrollments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_Name",
                table: "Memberships",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ApplicationUserId",
                table: "Enrollments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId_MembershipId",
                table: "Enrollments",
                columns: new[] { "UserId", "MembershipId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_ApplicationUserId",
                table: "Enrollments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Memberships_MembershipId",
                table: "Enrollments",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_ApplicationUserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Memberships_MembershipId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_Name",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_ApplicationUserId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId_MembershipId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "PurchasedAt",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "MembershipId",
                table: "Enrollments",
                newName: "GymClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_MembershipId",
                table: "Enrollments",
                newName: "IX_Enrollments_GymClassId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Memberships",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.CreateTable(
                name: "GymClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_GymClasses_GymClassId",
                table: "Enrollments",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
