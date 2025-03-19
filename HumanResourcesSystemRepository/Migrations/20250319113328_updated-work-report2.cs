using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesSystemRepository.Migrations
{
    /// <inheritdoc />
    public partial class updatedworkreport2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewerId",
                table: "WorkReports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkReports_ReviewerId",
                table: "WorkReports",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkReports_AspNetUsers_ReviewerId",
                table: "WorkReports",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkReports_AspNetUsers_ReviewerId",
                table: "WorkReports");

            migrationBuilder.DropIndex(
                name: "IX_WorkReports_ReviewerId",
                table: "WorkReports");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "WorkReports");
        }
    }
}
