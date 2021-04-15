using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewService.Infrastructure.Migrations
{
    public partial class AddedGuidToReviewEvaluations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalReviewLink",
                table: "ReviewEvaluations",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ReviewEvaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ReviewEvaluations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ReviewEvaluations",
                newName: "PersonalReviewLink");
        }
    }
}
