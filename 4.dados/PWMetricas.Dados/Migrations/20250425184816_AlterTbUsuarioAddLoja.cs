using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMetricas.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlterTbUsuarioAddLoja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario",
                column: "LojaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Loja_LojaId",
                table: "Usuario",
                column: "LojaId",
                principalTable: "Loja",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Loja_LojaId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Usuario");
        }
    }
}
