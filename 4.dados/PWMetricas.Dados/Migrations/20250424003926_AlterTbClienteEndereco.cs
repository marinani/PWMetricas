using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWMetricas.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AlterTbClienteEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroEndereço",
                table: "Cliente",
                newName: "NumeroEndereco");

            migrationBuilder.AlterColumn<string>(
                name: "EmailCobranca",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroEndereco",
                table: "Cliente",
                newName: "NumeroEndereço");

            migrationBuilder.AlterColumn<string>(
                name: "EmailCobranca",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
