using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AltaVisual.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantidadeAoBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Adesivo_Quantidade",
                table: "Produtos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adesivo_Quantidade",
                table: "Produtos");
        }
    }
}
