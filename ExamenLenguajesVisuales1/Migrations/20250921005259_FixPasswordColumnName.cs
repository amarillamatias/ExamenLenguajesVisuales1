using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamenLenguajesVisuales1.Migrations
{
    /// <inheritdoc />
    public partial class FixPasswordColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contrase�a",
                table: "Usuarios",
                newName: "contraseña");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contraseña",
                table: "Usuarios",
                newName: "contrase�a");
        }
    }
}
