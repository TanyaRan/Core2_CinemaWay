namespace CinemaWay.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserProjectionsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjection");

            migrationBuilder.CreateTable(
                name: "UserProjections",
                columns: table => new
                {
                    ProjectionId = table.Column<int>(nullable: false),
                    VisitorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjections", x => new { x.ProjectionId, x.VisitorId });
                    table.ForeignKey(
                        name: "FK_UserProjections_Projections_ProjectionId",
                        column: x => x.ProjectionId,
                        principalTable: "Projections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjections_AspNetUsers_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjections_VisitorId",
                table: "UserProjections",
                column: "VisitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjections");

            migrationBuilder.CreateTable(
                name: "UserProjection",
                columns: table => new
                {
                    ProjectionId = table.Column<int>(nullable: false),
                    VisitorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjection", x => new { x.ProjectionId, x.VisitorId });
                    table.ForeignKey(
                        name: "FK_UserProjection_Projections_ProjectionId",
                        column: x => x.ProjectionId,
                        principalTable: "Projections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjection_AspNetUsers_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjection_VisitorId",
                table: "UserProjection",
                column: "VisitorId");
        }
    }
}
