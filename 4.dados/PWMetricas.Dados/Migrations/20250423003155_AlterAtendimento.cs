using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMetricas.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlterAtendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "OrigemId",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusAtendimentoId",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TamanhoId",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_OrigemId",
                table: "Atendimento",
                column: "OrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_StatusAtendimentoId",
                table: "Atendimento",
                column: "StatusAtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_TamanhoId",
                table: "Atendimento",
                column: "TamanhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Origem_OrigemId",
                table: "Atendimento",
                column: "OrigemId",
                principalTable: "Origem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_StatusAtendimento_StatusAtendimentoId",
                table: "Atendimento",
                column: "StatusAtendimentoId",
                principalTable: "StatusAtendimento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Tamanho_TamanhoId",
                table: "Atendimento",
                column: "TamanhoId",
                principalTable: "Tamanho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Origem_OrigemId",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_StatusAtendimento_StatusAtendimentoId",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Tamanho_TamanhoId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_OrigemId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_StatusAtendimentoId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_TamanhoId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "OrigemId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "StatusAtendimentoId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "TamanhoId",
                table: "Atendimento");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Atendimento",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
