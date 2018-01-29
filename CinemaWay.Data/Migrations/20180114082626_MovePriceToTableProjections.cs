namespace CinemaWay.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MovePriceToTableProjections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "UserProjection");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Projections",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Projections");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "UserProjection",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
