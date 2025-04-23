using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMetricas.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlterAtendimentoEAddCorHex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Cidade_CidadeId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_CidadeId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "CidadeId",
                table: "Atendimento");

            migrationBuilder.AddColumn<string>(
                name: "CorHex",
                table: "Tamanho",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorHex",
                table: "StatusAtendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorHex",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorHex",
                table: "Origem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorHex",
                table: "Canal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Whatsapp",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorHex",
                table: "Tamanho");

            migrationBuilder.DropColumn(
                name: "CorHex",
                table: "StatusAtendimento");

            migrationBuilder.DropColumn(
                name: "CorHex",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CorHex",
                table: "Origem");

            migrationBuilder.DropColumn(
                name: "CorHex",
                table: "Canal");

            migrationBuilder.DropColumn(
                name: "Whatsapp",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "CidadeId",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_CidadeId",
                table: "Atendimento",
                column: "CidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Cidade_CidadeId",
                table: "Atendimento",
                column: "CidadeId",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
