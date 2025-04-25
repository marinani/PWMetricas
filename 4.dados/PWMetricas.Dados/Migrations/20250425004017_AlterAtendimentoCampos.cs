using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMetricas.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlterAtendimentoCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_LojaId",
                table: "Atendimento",
                column: "LojaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Loja_LojaId",
                table: "Atendimento",
                column: "LojaId",
                principalTable: "Loja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Loja_LojaId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_LojaId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "Atendimento");
        }
    }
}
