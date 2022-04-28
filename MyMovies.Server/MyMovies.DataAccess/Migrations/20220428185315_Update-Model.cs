using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMovies.DataAccess.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductionYear",
                table: "Movie",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,0)",
                oldMaxLength: 2100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionYear",
                table: "Movie",
                type: "decimal(4,0)",
                maxLength: 2100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
