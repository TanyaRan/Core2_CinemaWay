﻿namespace CinemaWay.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class GradeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "UserProjections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "UserProjections");
        }
    }
}
