using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdemGo.Migrations
{
    /// <inheritdoc />
    public partial class AddServicosToOrdensServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdensServicoServicos",
                schema: "dbo",
                columns: table => new
                {
                    OrdemServicoId = table.Column<int>(type: "int", nullable: false),
                    ServicoId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensServicoServicos", x => new { x.OrdemServicoId, x.ServicoId });
                    table.ForeignKey(
                        name: "FK_OrdensServicoServicos_OrdensServico_OrdemServicoId",
                        column: x => x.OrdemServicoId,
                        principalSchema: "dbo",
                        principalTable: "OrdensServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdensServicoServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalSchema: "dbo",
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServicoServicos_ServicoId",
                schema: "dbo",
                table: "OrdensServicoServicos",
                column: "ServicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdensServicoServicos",
                schema: "dbo");
        }
    }
}
