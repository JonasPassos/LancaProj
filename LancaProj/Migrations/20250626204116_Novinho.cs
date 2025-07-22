using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LancaProj.Migrations
{
    /// <inheritdoc />
    public partial class Novinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Funcao = table.Column<string>(type: "text", nullable: false),
                    Dia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adm", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataEvento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FunEntrada = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FunPausa = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FunVolta = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FunSaida = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FunFolga = table.Column<bool>(type: "boolean", nullable: false),
                    Cor = table.Column<string>(type: "text", nullable: false),
                    AdmID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Horarios_Adm_AdmID",
                        column: x => x.AdmID,
                        principalTable: "Adm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_AdmID",
                table: "Horarios",
                column: "AdmID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Adm");
        }
    }
}
