using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProArena.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResultadoPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Concluido",
                table: "Partidas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VencedorId",
                table: "Partidas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluido",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "VencedorId",
                table: "Partidas");
        }
    }
}
