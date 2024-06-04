using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoPagoWeb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatosPago",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    api_version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_created = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    live_mode = table.Column<bool>(type: "bit", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosPago", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosPago");
        }
    }
}
