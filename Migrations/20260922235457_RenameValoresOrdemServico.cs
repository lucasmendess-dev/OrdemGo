using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdemGo.Migrations
{
    /// <inheritdoc />
    public partial class RenameValoresOrdemServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Acrescimo",
                schema: "dbo",
                table: "OrdensServico",
                newName: "ValorPecas");

            migrationBuilder.RenameColumn(
                name: "Desconto",
                schema: "dbo",
                table: "OrdensServico",
                newName: "ValorServico");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPecas",
                schema: "dbo",
                table: "OrdensServico",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorServico",
                schema: "dbo",
                table: "OrdensServico",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPecas",
                schema: "dbo",
                table: "OrdensServico",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorServico",
                schema: "dbo",
                table: "OrdensServico",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.RenameColumn(
                name: "ValorPecas",
                schema: "dbo",
                table: "OrdensServico",
                newName: "Acrescimo");

            migrationBuilder.RenameColumn(
                name: "ValorServico",
                schema: "dbo",
                table: "OrdensServico",
                newName: "Desconto");
        }
    }
}
