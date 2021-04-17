using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewService.Infrastructure.Migrations
{
    public partial class RenamedReviewMasterToReviewManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewMaster",
                table: "ReviewSessions",
                newName: "ReviewManager");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewManager",
                table: "ReviewSessions",
                newName: "ReviewMaster");
        }
    }
}
