using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnkrStore.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPedidosCupom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cupom",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cupom",
                table: "Pedidos");
        }
    }
}
