using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProArena.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campeonatos",
                columns: table => new
                {
                    CampeonatoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campeonatos", x => x.CampeonatoId);
                });

            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    JogadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.JogadorId);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    EquipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CampeonatoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.EquipeId);
                    table.ForeignKey(
                        name: "FK_Equipes_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "CampeonatoId");
                });

            migrationBuilder.CreateTable(
                name: "EquipeJogador",
                columns: table => new
                {
                    EquipesEquipeId = table.Column<int>(type: "int", nullable: false),
                    JogadoresJogadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeJogador", x => new { x.EquipesEquipeId, x.JogadoresJogadorId });
                    table.ForeignKey(
                        name: "FK_EquipeJogador_Equipes_EquipesEquipeId",
                        column: x => x.EquipesEquipeId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipeJogador_Jogadores_JogadoresJogadorId",
                        column: x => x.JogadoresJogadorId,
                        principalTable: "Jogadores",
                        principalColumn: "JogadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    PartidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultadoEquipeA = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    ResultadoEquipeB = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipeAId = table.Column<int>(type: "int", nullable: false),
                    EquipeBId = table.Column<int>(type: "int", nullable: false),
                    CampeonatoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.PartidaId);
                    table.ForeignKey(
                        name: "FK_Partidas_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "CampeonatoId");
                    table.ForeignKey(
                        name: "FK_Partidas_Equipes_EquipeAId",
                        column: x => x.EquipeAId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidas_Equipes_EquipeBId",
                        column: x => x.EquipeBId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeJogador_JogadoresJogadorId",
                table: "EquipeJogador",
                column: "JogadoresJogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_CampeonatoId",
                table: "Equipes",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_CampeonatoId",
                table: "Partidas",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_EquipeAId",
                table: "Partidas",
                column: "EquipeAId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_EquipeBId",
                table: "Partidas",
                column: "EquipeBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipeJogador");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Jogadores");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Campeonatos");
        }
    }
}
