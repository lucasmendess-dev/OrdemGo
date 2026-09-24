using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdemGo.Migrations
{
    /// <inheritdoc />
    public partial class ConvertTempoEstimadoParaHoras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TempoEstimadoMinutos",
                schema: "dbo",
                table: "Servicos",
                newName: "TempoEstimadoHoras");

            migrationBuilder.AlterColumn<decimal>(
                name: "TempoEstimadoHoras",
                schema: "dbo",
                table: "Servicos",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql(
                "UPDATE [dbo].[Servicos] SET [TempoEstimadoHoras] = [TempoEstimadoHoras] / 60.0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE [dbo].[Servicos] SET [TempoEstimadoHoras] = ROUND([TempoEstimadoHoras] * 60.0, 0);");

            migrationBuilder.AlterColumn<int>(
                name: "TempoEstimadoHoras",
                schema: "dbo",
                table: "Servicos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.RenameColumn(
                name: "TempoEstimadoHoras",
                schema: "dbo",
                table: "Servicos",
                newName: "TempoEstimadoMinutos");
        }
    }
}
