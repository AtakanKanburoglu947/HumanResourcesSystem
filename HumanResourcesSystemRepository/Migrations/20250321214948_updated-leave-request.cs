using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesSystemRepository.Migrations
{
    /// <inheritdoc />
    public partial class updatedleaverequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "LeaveRequests",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "LeaveRequests");
        }
    }
}
