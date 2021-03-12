using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewService.Infrastructure.Migrations
{
    public partial class Added_ReviewTemplate_EvPointsTemplate_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationPointItems");

            migrationBuilder.AddColumn<int>(
                name: "EvaluationPointsTemplateId",
                table: "EvaluationPoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EvaluationPointsTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPointsTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationPointsTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewTemplates_EvaluationPointsTemplates_EvaluationPointsTemplateId",
                        column: x => x.EvaluationPointsTemplateId,
                        principalTable: "EvaluationPointsTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreaReviewTemplate",
                columns: table => new
                {
                    AreasId = table.Column<int>(type: "int", nullable: false),
                    ReviewTemplatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaReviewTemplate", x => new { x.AreasId, x.ReviewTemplatesId });
                    table.ForeignKey(
                        name: "FK_AreaReviewTemplate_Areas_AreasId",
                        column: x => x.AreasId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaReviewTemplate_ReviewTemplates_ReviewTemplatesId",
                        column: x => x.ReviewTemplatesId,
                        principalTable: "ReviewTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPoints_EvaluationPointsTemplateId",
                table: "EvaluationPoints",
                column: "EvaluationPointsTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaReviewTemplate_ReviewTemplatesId",
                table: "AreaReviewTemplate",
                column: "ReviewTemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTemplates_EvaluationPointsTemplateId",
                table: "ReviewTemplates",
                column: "EvaluationPointsTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationPoints_EvaluationPointsTemplates_EvaluationPointsTemplateId",
                table: "EvaluationPoints",
                column: "EvaluationPointsTemplateId",
                principalTable: "EvaluationPointsTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationPoints_EvaluationPointsTemplates_EvaluationPointsTemplateId",
                table: "EvaluationPoints");

            migrationBuilder.DropTable(
                name: "AreaReviewTemplate");

            migrationBuilder.DropTable(
                name: "ReviewTemplates");

            migrationBuilder.DropTable(
                name: "EvaluationPointsTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationPoints_EvaluationPointsTemplateId",
                table: "EvaluationPoints");

            migrationBuilder.DropColumn(
                name: "EvaluationPointsTemplateId",
                table: "EvaluationPoints");

            migrationBuilder.CreateTable(
                name: "EvaluationPointItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationPointId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPointItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationPointItems_EvaluationPoints_EvaluationPointId",
                        column: x => x.EvaluationPointId,
                        principalTable: "EvaluationPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPointItems_EvaluationPointId",
                table: "EvaluationPointItems",
                column: "EvaluationPointId");
        }
    }
}
