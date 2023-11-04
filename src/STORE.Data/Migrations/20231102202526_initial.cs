using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STORE.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    ArticuloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.ArticuloID);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Tienda",
                columns: table => new
                {
                    TiendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sucursal = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tienda", x => x.TiendaID);
                });

            migrationBuilder.CreateTable(
                name: "ClienteArticulo",
                columns: table => new
                {
                    ClienteArticuloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    ArticuloID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteArticulo", x => x.ClienteArticuloID);
                    table.ForeignKey(
                        name: "FK_ClienteArticulo_Articulo_ArticuloID",
                        column: x => x.ArticuloID,
                        principalTable: "Articulo",
                        principalColumn: "ArticuloID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteArticulo_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticuloTienda",
                columns: table => new
                {
                    ArticuloTiendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloID = table.Column<int>(type: "int", nullable: false),
                    TiendaID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloTienda", x => x.ArticuloTiendaID);
                    table.ForeignKey(
                        name: "FK_ArticuloTienda_Articulo_ArticuloID",
                        column: x => x.ArticuloID,
                        principalTable: "Articulo",
                        principalColumn: "ArticuloID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticuloTienda_Tienda_TiendaID",
                        column: x => x.TiendaID,
                        principalTable: "Tienda",
                        principalColumn: "TiendaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloTienda_ArticuloID",
                table: "ArticuloTienda",
                column: "ArticuloID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloTienda_TiendaID",
                table: "ArticuloTienda",
                column: "TiendaID");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteArticulo_ArticuloID",
                table: "ClienteArticulo",
                column: "ArticuloID");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteArticulo_ClienteID",
                table: "ClienteArticulo",
                column: "ClienteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloTienda");

            migrationBuilder.DropTable(
                name: "ClienteArticulo");

            migrationBuilder.DropTable(
                name: "Tienda");

            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
