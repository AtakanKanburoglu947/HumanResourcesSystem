using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HumanResourcesSystemRepository.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

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

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReviewerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandiadateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidatePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResumeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b25e9802-b2ab-4228-89bc-fa0f02ce00c9", null, "User", "USER" },
                    { "b94c68a0-42c2-41ae-bf4f-342dfbd33522", null, "Manager", "MANAGER" },
                    { "e1f5bc6f-c44e-4b5d-8a8f-8d77e600d93f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ReviewerId",
                table: "JobApplications",
                column: "ReviewerId");
        }
    }
}
