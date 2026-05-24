using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProArena.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class equipesjogadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipePartida_Equipes_EquipeId",
                table: "EquipePartida");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Campeonatos_CampeonatoId",
                table: "Inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Equipes_EquipeId",
                table: "Inscricao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricao",
                table: "Inscricao");

            migrationBuilder.DropColumn(
                name: "EquipeId",
                table: "Partidas");

            migrationBuilder.RenameTable(
                name: "Inscricao",
                newName: "Inscricoes");

            migrationBuilder.RenameColumn(
                name: "EquipeId",
                table: "EquipePartida",
                newName: "EquipesEquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscricao_EquipeId",
                table: "Inscricoes",
                newName: "IX_Inscricoes_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscricao_CampeonatoId",
                table: "Inscricoes",
                newName: "IX_Inscricoes_CampeonatoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes",
                column: "InscricaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipePartida_Equipes_EquipesEquipeId",
                table: "EquipePartida",
                column: "EquipesEquipeId",
                principalTable: "Equipes",
                principalColumn: "EquipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Campeonatos_CampeonatoId",
                table: "Inscricoes",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "CampeonatoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Equipes_EquipeId",
                table: "Inscricoes",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "EquipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipePartida_Equipes_EquipesEquipeId",
                table: "EquipePartida");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Campeonatos_CampeonatoId",
                table: "Inscricoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Equipes_EquipeId",
                table: "Inscricoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes");

            migrationBuilder.RenameTable(
                name: "Inscricoes",
                newName: "Inscricao");

            migrationBuilder.RenameColumn(
                name: "EquipesEquipeId",
                table: "EquipePartida",
                newName: "EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscricoes_EquipeId",
                table: "Inscricao",
                newName: "IX_Inscricao_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscricoes_CampeonatoId",
                table: "Inscricao",
                newName: "IX_Inscricao_CampeonatoId");

            migrationBuilder.AddColumn<int>(
                name: "EquipeId",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricao",
                table: "Inscricao",
                column: "InscricaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipePartida_Equipes_EquipeId",
                table: "EquipePartida",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "EquipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Campeonatos_CampeonatoId",
                table: "Inscricao",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "CampeonatoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Equipes_EquipeId",
                table: "Inscricao",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "EquipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
