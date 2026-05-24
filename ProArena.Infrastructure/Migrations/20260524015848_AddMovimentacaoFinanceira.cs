using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProArena.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMovimentacaoFinanceira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentacoesFinanceiras",
                columns: table => new
                {
                    MovimentacaoFinanceiraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CampeonatoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesFinanceiras", x => x.MovimentacaoFinanceiraId);
                    table.ForeignKey(
                        name: "FK_MovimentacoesFinanceiras_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "CampeonatoId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesFinanceiras_CampeonatoId",
                table: "MovimentacoesFinanceiras",
                column: "CampeonatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacoesFinanceiras");
        }
    }
}
