using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMetricas.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlterTbLoja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Loja",
                newName: "NomeFantasia");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailCobranca",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroEndereço",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelEmpresa",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Loja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "EmailCobranca",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "NumeroEndereço",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "ResponsavelEmpresa",
                table: "Loja");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Loja");

            migrationBuilder.RenameColumn(
                name: "Uf",
                table: "Loja",
                newName: "Nome");
        }
    }
}
