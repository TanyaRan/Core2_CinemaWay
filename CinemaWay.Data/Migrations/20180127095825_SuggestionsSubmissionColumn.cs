namespace CinemaWay.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SuggestionsSubmissionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SuggestionsSubmission",
                table: "UserProjections",
                maxLength: 2097152,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuggestionsSubmission",
                table: "UserProjections");
        }
    }
}
