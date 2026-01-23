using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProArena.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorigindoRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_Campeonatos_CampeonatoId",
                table: "Equipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Campeonatos_CampeonatoId",
                table: "Partidas");

            migrationBuilder.AlterColumn<int>(
                name: "CampeonatoId",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Campeonatos_CampeonatoId",
                table: "Equipes",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "CampeonatoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Campeonatos_CampeonatoId",
                table: "Partidas",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "CampeonatoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_Campeonatos_CampeonatoId",
                table: "Equipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Campeonatos_CampeonatoId",
                table: "Partidas");

            migrationBuilder.AlterColumn<int>(
                name: "CampeonatoId",
                table: "Partidas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Campeonatos_CampeonatoId",
                table: "Equipes",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "CampeonatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Campeonatos_CampeonatoId",
                table: "Partidas",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "CampeonatoId");
        }
    }
}
