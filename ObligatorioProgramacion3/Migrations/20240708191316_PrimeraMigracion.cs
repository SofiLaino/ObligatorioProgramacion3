using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ObligatorioProgramacion3.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMaquina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMaquina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoRutinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRutinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSocios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSocios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locales_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locales_Responsables_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Responsables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rutinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoRutinaId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rutinas_TipoRutinas_TipoRutinaId",
                        column: x => x.TipoRutinaId,
                        principalTable: "TipoRutinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maquinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalAsociadoId = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioCompra = table.Column<int>(type: "int", nullable: false),
                    VidaUtil = table.Column<int>(type: "int", nullable: false),
                    TipoMaquinaId = table.Column<int>(type: "int", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maquinas_Locales_LocalAsociadoId",
                        column: x => x.LocalAsociadoId,
                        principalTable: "Locales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maquinas_TipoMaquina_TipoMaquinaId",
                        column: x => x.TipoMaquinaId,
                        principalTable: "TipoMaquina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoSocioId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalAsociadoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Socios_Locales_LocalAsociadoId",
                        column: x => x.LocalAsociadoId,
                        principalTable: "Locales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Socios_TipoSocios_TipoSocioId",
                        column: x => x.TipoSocioId,
                        principalTable: "TipoSocios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ejercicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaquinaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ejercicios_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RutinasSocios",
                columns: table => new
                {
                    IdRutina = table.Column<int>(type: "int", nullable: false),
                    IdSocio = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutinasSocios", x => new { x.IdRutina, x.IdSocio });
                    table.ForeignKey(
                        name: "FK_RutinasSocios_Rutinas_IdRutina",
                        column: x => x.IdRutina,
                        principalTable: "Rutinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RutinasSocios_Socios_IdSocio",
                        column: x => x.IdSocio,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RutinasEjercicios",
                columns: table => new
                {
                    IdRutina = table.Column<int>(type: "int", nullable: false),
                    IdEjercicio = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutinasEjercicios", x => new { x.IdRutina, x.IdEjercicio });
                    table.ForeignKey(
                        name: "FK_RutinasEjercicios_Ejercicios_IdEjercicio",
                        column: x => x.IdEjercicio,
                        principalTable: "Ejercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RutinasEjercicios_Rutinas_IdRutina",
                        column: x => x.IdRutina,
                        principalTable: "Rutinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ciudades",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Tarariras" },
                    { 2, "Colonia del Sacramento" },
                    { 3, "Juan Lacaze" },
                    { 4, "Rosario" }
                });

            migrationBuilder.InsertData(
                table: "Responsables",
                columns: new[] { "Id", "Apellido", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Pellaton", "Agustina", "091672542" },
                    { 2, "Perez", "Juan", "091655818" }
                });

            migrationBuilder.InsertData(
                table: "TipoMaquina",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Fuerza" },
                    { 2, "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "TipoRutinas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Bajar de peso" },
                    { 2, "Aumentar masa muscular" },
                    { 3, "Definicion" }
                });

            migrationBuilder.InsertData(
                table: "TipoSocios",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Premium" },
                    { 2, "Standard" }
                });

            migrationBuilder.InsertData(
                table: "Locales",
                columns: new[] { "Id", "CiudadId", "Direccion", "Nombre", "ResponsableId", "Telefono" },
                values: new object[,]
                {
                    { 1, 1, "La paz 2138", "Mega", 1, "098543124" },
                    { 2, 2, "Mangaeli 1234", "Oxigeno", 2, "091453122" }
                });

            migrationBuilder.InsertData(
                table: "Rutinas",
                columns: new[] { "Id", "Descripcion", "Nombre", "TipoRutinaId" },
                values: new object[,]
                {
                    { 1, "Semanal", "Full", 1 },
                    { 2, "Semanal", "Pierna", 2 }
                });

            migrationBuilder.InsertData(
                table: "Maquinas",
                columns: new[] { "Id", "Disponible", "FechaCompra", "LocalAsociadoId", "Nombre", "PrecioCompra", "TipoMaquinaId", "VidaUtil" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Caminadora", 12000, 2, 7 },
                    { 2, false, new DateTime(2019, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Prensa", 20000, 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "Socios",
                columns: new[] { "Id", "Apellido", "Email", "LocalAsociadoId", "Nombre", "Telefono", "TipoSocioId" },
                values: new object[,]
                {
                    { 1, "Pellaton", "agusPellaton@gmail.com", 1, "Agustina", "091672542", 2 },
                    { 2, "Geymonat", "fioGeymonat@gmail.com", 1, "Fiorella", "098564234", 2 },
                    { 3, "Laino", "sofiaLaino@gmail.com", 2, "Sofia", "099765234", 1 }
                });

            migrationBuilder.InsertData(
                table: "Ejercicios",
                columns: new[] { "Id", "MaquinaId", "Nombre" },
                values: new object[,]
                {
                    { 1, 2, "Caminar" },
                    { 2, 2, "Correr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicios_MaquinaId",
                table: "Ejercicios",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Locales_CiudadId",
                table: "Locales",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Locales_ResponsableId",
                table: "Locales",
                column: "ResponsableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_LocalAsociadoId",
                table: "Maquinas",
                column: "LocalAsociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_TipoMaquinaId",
                table: "Maquinas",
                column: "TipoMaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutinas_TipoRutinaId",
                table: "Rutinas",
                column: "TipoRutinaId");

            migrationBuilder.CreateIndex(
                name: "IX_RutinasEjercicios_IdEjercicio",
                table: "RutinasEjercicios",
                column: "IdEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_RutinasSocios_IdSocio",
                table: "RutinasSocios",
                column: "IdSocio");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_LocalAsociadoId",
                table: "Socios",
                column: "LocalAsociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_TipoSocioId",
                table: "Socios",
                column: "TipoSocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RutinasEjercicios");

            migrationBuilder.DropTable(
                name: "RutinasSocios");

            migrationBuilder.DropTable(
                name: "Ejercicios");

            migrationBuilder.DropTable(
                name: "Rutinas");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Maquinas");

            migrationBuilder.DropTable(
                name: "TipoRutinas");

            migrationBuilder.DropTable(
                name: "TipoSocios");

            migrationBuilder.DropTable(
                name: "Locales");

            migrationBuilder.DropTable(
                name: "TipoMaquina");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Responsables");
        }
    }
}
