using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HumanResourcesSystemRepository.Migrations
{
    /// <inheritdoc />
    public partial class addeddailytasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f479490-5a7e-4f7c-8f81-5b120bb89197");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8521cee5-df90-472c-a8dd-822bbf550acf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f62741cb-69c3-49f4-91a1-79a4a4299110");

            migrationBuilder.CreateTable(
                name: "DailyTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isFinished = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4abcbe5c-261b-4b41-8955-c95fdee2de25", null, "User", "USER" },
                    { "51e2fe06-2492-41cf-9822-658ad66f315f", null, "Admin", "ADMIN" },
                    { "ef39be9d-347c-40cd-a4ee-61ae7addb07a", null, "Manager", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_UserId",
                table: "DailyTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4abcbe5c-261b-4b41-8955-c95fdee2de25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51e2fe06-2492-41cf-9822-658ad66f315f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef39be9d-347c-40cd-a4ee-61ae7addb07a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f479490-5a7e-4f7c-8f81-5b120bb89197", null, "Admin", "ADMIN" },
                    { "8521cee5-df90-472c-a8dd-822bbf550acf", null, "Manager", "MANAGER" },
                    { "f62741cb-69c3-49f4-91a1-79a4a4299110", null, "User", "USER" }
                });
        }
    }
}
