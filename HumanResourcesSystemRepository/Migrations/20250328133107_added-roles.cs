using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HumanResourcesSystemRepository.Migrations
{
    /// <inheritdoc />
    public partial class addedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b25e9802-b2ab-4228-89bc-fa0f02ce00c9", null, "User", "USER" },
                    { "b94c68a0-42c2-41ae-bf4f-342dfbd33522", null, "Manager", "MANAGER" },
                    { "e1f5bc6f-c44e-4b5d-8a8f-8d77e600d93f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b25e9802-b2ab-4228-89bc-fa0f02ce00c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b94c68a0-42c2-41ae-bf4f-342dfbd33522");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1f5bc6f-c44e-4b5d-8a8f-8d77e600d93f");
        }
    }
}
