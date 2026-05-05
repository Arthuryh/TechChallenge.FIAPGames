using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BibliotecaJogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bibliotecas",
                columns: table => new
                {
                    IdBiblioteca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdConta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibliotecas", x => x.IdBiblioteca);
                })
                .Annotation("MySql:CharSet", "utf8mb4");


            migrationBuilder.CreateTable(
                name: "BibliotecaJogos",
                columns: table => new
                {
                    BibliotecaId = table.Column<int>(type: "int", nullable: false),
                    JogoId = table.Column<int>(type: "int", nullable: false),
                    DataAdicao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaJogos", x => new { x.BibliotecaId, x.JogoId });
                    table.ForeignKey(
                        name: "FK_BibliotecaJogos_Bibliotecas_BibliotecaId",
                        column: x => x.BibliotecaId,
                        principalTable: "Bibliotecas",
                        principalColumn: "IdBiblioteca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecaJogos_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaJogos_JogoId",
                table: "BibliotecaJogos",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibliotecaJogos");

            migrationBuilder.DropTable(
                name: "Bibliotecas");
        }
    }
}
