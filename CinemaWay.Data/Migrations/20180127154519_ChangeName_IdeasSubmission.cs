namespace CinemaWay.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeName_IdeasSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuggestionsSubmission",
                table: "UserProjections",
                newName: "IdeasSubmission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdeasSubmission",
                table: "UserProjections",
                newName: "SuggestionsSubmission");
        }
    }
}
