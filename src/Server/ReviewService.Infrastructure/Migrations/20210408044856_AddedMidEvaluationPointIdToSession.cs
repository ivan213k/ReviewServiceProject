using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewService.Infrastructure.Migrations
{
    public partial class AddedMidEvaluationPointIdToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationPointsTemplateId",
                table: "ReviewSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MidEvaluationPointId",
                table: "ReviewSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluationPointsTemplateId",
                table: "ReviewSessions");

            migrationBuilder.DropColumn(
                name: "MidEvaluationPointId",
                table: "ReviewSessions");
        }
    }
}
