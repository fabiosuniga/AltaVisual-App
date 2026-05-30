using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaVisual.Migrations
{
    /// <inheritdoc />
    public partial class AddMetrosUsadosAdesivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MetrosQuadradosUsados",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetrosQuadradosUsados",
                table: "Produtos");
        }
    }
}
