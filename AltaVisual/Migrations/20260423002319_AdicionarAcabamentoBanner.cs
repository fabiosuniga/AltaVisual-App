using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaVisual.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarAcabamentoBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Acabamento",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acabamento",
                table: "Produtos");
        }
    }
}
