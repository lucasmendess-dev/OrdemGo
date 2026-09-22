using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdemGo.Migrations
{
    /// <inheritdoc />
    public partial class RenameOrdemFieldsAndGenerateNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Equipamento",
                schema: "dbo",
                table: "OrdensServico",
                newName: "Modelo");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                schema: "dbo",
                table: "OrdensServico",
                newName: "Equipamento");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroOS",
                schema: "dbo",
                table: "OrdensServico",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                computedColumnSql: "('#OS-' + REPLICATE('0', CASE WHEN LEN(CONVERT(varchar(20), [Id])) < 5 THEN 5 - LEN(CONVERT(varchar(20), [Id])) ELSE 0 END) + CONVERT(varchar(20), [Id]))",
                stored: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Equipamento",
                schema: "dbo",
                table: "OrdensServico",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                schema: "dbo",
                table: "OrdensServico",
                newName: "Equipamento");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroOS",
                schema: "dbo",
                table: "OrdensServico",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldComputedColumnSql: "('#OS-' + REPLICATE('0', CASE WHEN LEN(CONVERT(varchar(20), [Id])) < 5 THEN 5 - LEN(CONVERT(varchar(20), [Id])) ELSE 0 END) + CONVERT(varchar(20), [Id]))");
        }
    }
}
