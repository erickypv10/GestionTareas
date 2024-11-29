using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_de_Tareas.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completada",
                table: "Tareas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completada",
                table: "Tareas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
