using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewService.Infrastructure.Migrations
{
    public partial class AddedMidEvaluationPointToReviewTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MidEvaluationPointId",
                table: "ReviewTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTemplates_MidEvaluationPointId",
                table: "ReviewTemplates",
                column: "MidEvaluationPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTemplates_EvaluationPoints_MidEvaluationPointId",
                table: "ReviewTemplates",
                column: "MidEvaluationPointId",
                principalTable: "EvaluationPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTemplates_EvaluationPoints_MidEvaluationPointId",
                table: "ReviewTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ReviewTemplates_MidEvaluationPointId",
                table: "ReviewTemplates");

            migrationBuilder.DropColumn(
                name: "MidEvaluationPointId",
                table: "ReviewTemplates");
        }
    }
}
