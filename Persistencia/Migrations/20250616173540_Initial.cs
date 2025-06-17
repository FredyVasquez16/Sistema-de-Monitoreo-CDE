using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas_asesorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas_asesorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asesores_clientes_empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    cliente_empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asesores_clientes_empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetroles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrencystamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetroles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    nombrecompleto = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedusername = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedemail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    emailconfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    passwordhash = table.Column<string>(type: "text", nullable: true),
                    securitystamp = table.Column<string>(type: "text", nullable: true),
                    concurrencystamp = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true),
                    phonenumberconfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    twofactorenabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockoutenabled = table.Column<bool>(type: "boolean", nullable: false),
                    accessfailedcount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetusers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categorias_laborales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorias_laborales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cuotas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    precio = table.Column<decimal>(type: "numeric", nullable: false),
                    publicado = table.Column<bool>(type: "boolean", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cuotas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados_civiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estados_civiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "formatos_programa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_formatos_programa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fuentes_financiamiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fuentes_financiamiento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "niveles_estudio",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_niveles_estudio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "niveles_formalizacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_niveles_formalizacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "servicios_solicitados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicios_solicitados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sesiones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_final = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cuota_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sesiones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tamano_empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tamano_empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "temas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_cliente_nivel",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_cliente_nivel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_clientes_estado",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_clientes_estado", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_comercio_internacional",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_comercio_internacional", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_contabilidad",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_contabilidad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_contacto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_contacto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_empresa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_empresa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_organizacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos_organizacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetroleclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<string>(type: "text", nullable: false),
                    claimtype = table.Column<string>(type: "text", nullable: true),
                    claimvalue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetroleclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetroleclaims_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    claimtype = table.Column<string>(type: "text", nullable: true),
                    claimvalue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetuserclaims_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserlogins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(type: "text", nullable: false),
                    providerkey = table.Column<string>(type: "text", nullable: false),
                    providerdisplayname = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserlogins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "fk_aspnetuserlogins_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserroles",
                columns: table => new
                {
                    userid = table.Column<string>(type: "text", nullable: false),
                    roleid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserroles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_aspnetuserroles_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_aspnetuserroles_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusertokens",
                columns: table => new
                {
                    userid = table.Column<string>(type: "text", nullable: false),
                    loginprovider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetusertokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "fk_aspnetusertokens_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cuotassesiones",
                columns: table => new
                {
                    cuotasid = table.Column<int>(type: "integer", nullable: false),
                    sesionesid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cuotassesiones", x => new { x.cuotasid, x.sesionesid });
                    table.ForeignKey(
                        name: "fk_cuotassesiones_cuotas_cuotasid",
                        column: x => x.cuotasid,
                        principalTable: "cuotas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cuotassesiones_sesiones_sesionesid",
                        column: x => x.sesionesid,
                        principalTable: "sesiones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "capacitaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_id = table.Column<int>(type: "integer", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_cierre = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_informes = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hora_programada = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    total_horas = table.Column<int>(type: "integer", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    tema_principal_id = table.Column<int>(type: "integer", nullable: false),
                    formato_programa_id = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: true),
                    numero_max_participantes = table.Column<int>(type: "integer", nullable: true),
                    numero_sesiones = table.Column<int>(type: "integer", nullable: false),
                    direccion = table.Column<string>(type: "text", nullable: false),
                    ciudad = table.Column<string>(type: "text", nullable: false),
                    departamento = table.Column<string>(type: "text", nullable: false),
                    lugar_desarrollo = table.Column<string>(type: "text", nullable: false),
                    centro = table.Column<string>(type: "text", nullable: true),
                    patrocinio_centro = table.Column<bool>(type: "boolean", nullable: false),
                    co_patrocinios = table.Column<string>(type: "text", nullable: true),
                    recursos = table.Column<string>(type: "text", nullable: true),
                    contacto = table.Column<string>(type: "text", nullable: true),
                    correo_contacto = table.Column<string>(type: "text", nullable: true),
                    telefono_contacto = table.Column<int>(type: "integer", nullable: true),
                    idioma = table.Column<string>(type: "text", nullable: true),
                    unidad_historica = table.Column<string>(type: "text", nullable: true),
                    fuente_financiamiento_id = table.Column<int>(type: "integer", nullable: true),
                    instrucciones_asistente = table.Column<string>(type: "text", nullable: true),
                    notas = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capacitaciones", x => x.id);
                    table.ForeignKey(
                        name: "fk_capacitaciones_formatos_programa_formato_programa_id",
                        column: x => x.formato_programa_id,
                        principalTable: "formatos_programa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_capacitaciones_fuentes_financiamiento_fuente_financiamiento~",
                        column: x => x.fuente_financiamiento_id,
                        principalTable: "fuentes_financiamiento",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_capacitaciones_temas_tema_principal_id",
                        column: x => x.tema_principal_id,
                        principalTable: "temas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_capacitaciones_tipos_tipo_id",
                        column: x => x.tipo_id,
                        principalTable: "tipos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "capacitaciones_archivos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capacitacion_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_original = table.Column<string>(type: "text", nullable: false),
                    contenido = table.Column<byte[]>(type: "bytea", nullable: false),
                    tipo_mime = table.Column<string>(type: "text", nullable: false),
                    fecha_subida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capacitaciones_archivos", x => x.id);
                    table.ForeignKey(
                        name: "fk_capacitaciones_archivos_capacitaciones_capacitacion_id",
                        column: x => x.capacitacion_id,
                        principalTable: "capacitaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "capacitaciones_temas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capacitacion_id = table.Column<int>(type: "integer", nullable: false),
                    tema_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capacitaciones_temas", x => x.id);
                    table.ForeignKey(
                        name: "fk_capacitaciones_temas_capacitaciones_capacitacion_id",
                        column: x => x.capacitacion_id,
                        principalTable: "capacitaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_capacitaciones_temas_temas_tema_id",
                        column: x => x.tema_id,
                        principalTable: "temas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sesiones_capacitacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sesion_id = table.Column<int>(type: "integer", nullable: false),
                    capacitacion_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sesiones_capacitacion", x => x.id);
                    table.ForeignKey(
                        name: "fk_sesiones_capacitacion_capacitaciones_capacitacion_id",
                        column: x => x.capacitacion_id,
                        principalTable: "capacitaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sesiones_capacitacion_sesiones_sesion_id",
                        column: x => x.sesion_id,
                        principalTable: "sesiones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asesorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cliente_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_sesion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tiempo_contacto = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    tipo_contacto_id = table.Column<int>(type: "integer", nullable: false),
                    area_asesoria_id = table.Column<int>(type: "integer", nullable: false),
                    ayuda_adicional = table.Column<string>(type: "text", nullable: true),
                    asunto = table.Column<string>(type: "text", nullable: true),
                    fuente_financiamiento_id = table.Column<int>(type: "integer", nullable: false),
                    centro = table.Column<string>(type: "text", nullable: true),
                    numero_participantes = table.Column<int>(type: "integer", nullable: true),
                    notas = table.Column<string>(type: "text", nullable: true),
                    referido_a = table.Column<string>(type: "text", nullable: true),
                    descripcion_referido = table.Column<string>(type: "text", nullable: true),
                    descripcion_derivado = table.Column<string>(type: "text", nullable: true),
                    descripcion_asesoria_especializada = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asesorias", x => x.id);
                    table.ForeignKey(
                        name: "fk_asesorias_areas_asesorias_area_asesoria_id",
                        column: x => x.area_asesoria_id,
                        principalTable: "areas_asesorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asesorias_fuentes_financiamiento_fuente_financiamiento_id",
                        column: x => x.fuente_financiamiento_id,
                        principalTable: "fuentes_financiamiento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asesorias_tipo_contacto",
                        column: x => x.tipo_contacto_id,
                        principalTable: "tipos_contacto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asesorias_archivos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    asesoria_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_original = table.Column<string>(type: "text", nullable: false),
                    contenido = table.Column<byte[]>(type: "bytea", nullable: false),
                    tipo_mime = table.Column<string>(type: "text", nullable: false),
                    fecha_subida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asesorias_archivos", x => x.id);
                    table.ForeignKey(
                        name: "fk_asesorias_archivos_asesorias_asesoria_id",
                        column: x => x.asesoria_id,
                        principalTable: "asesorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asesorias_asesores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    asesoria_id = table.Column<int>(type: "integer", nullable: false),
                    asesor_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asesorias_asesores", x => x.id);
                    table.ForeignKey(
                        name: "fk_asesorias_asesores_asesorias_asesoria_id",
                        column: x => x.asesoria_id,
                        principalTable: "asesorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asesorias_asesores_aspnetusers_asesor_id",
                        column: x => x.asesor_id,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asesorias_contactos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contacto_id = table.Column<int>(type: "integer", nullable: false),
                    asesoria_id = table.Column<int>(type: "integer", nullable: false),
                    cliente_empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asesorias_contactos", x => x.id);
                    table.ForeignKey(
                        name: "fk_asesorias_contactos_asesorias_asesoria_id",
                        column: x => x.asesoria_id,
                        principalTable: "asesorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes_empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    tipo_cliente_nivel_id = table.Column<int>(type: "integer", nullable: false),
                    contacto_primario_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_cliente_estado_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    servicio_solicitado_id = table.Column<int>(type: "integer", nullable: false),
                    razon_social = table.Column<string>(type: "text", nullable: true),
                    telefono = table.Column<int>(type: "integer", nullable: false),
                    correo = table.Column<string>(type: "text", nullable: false),
                    pagina_web = table.Column<string>(type: "text", nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    direccion_fisica = table.Column<string>(type: "text", nullable: false),
                    ciudad = table.Column<string>(type: "text", nullable: false),
                    departamento = table.Column<string>(type: "text", nullable: false),
                    tipo_organizacion_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_empresa_id = table.Column<int>(type: "integer", nullable: false),
                    tamano_empresa_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_contabilidad_id = table.Column<int>(type: "integer", nullable: false),
                    nivel_formalizacion_id = table.Column<int>(type: "integer", nullable: false),
                    participa_gremio = table.Column<bool>(type: "boolean", nullable: false),
                    beneficiado_cde = table.Column<bool>(type: "boolean", nullable: false),
                    tipo_casos_en_proceso = table.Column<string>(type: "text", nullable: true),
                    empleados_tiempo_completo = table.Column<int>(type: "integer", nullable: false),
                    empleados_medio_tiempo = table.Column<int>(type: "integer", nullable: true),
                    trabajadores_informales = table.Column<int>(type: "integer", nullable: true),
                    negocio_en_linea = table.Column<bool>(type: "boolean", nullable: false),
                    negocio_en_casa = table.Column<bool>(type: "boolean", nullable: false),
                    comercio_internacional_id = table.Column<int>(type: "integer", nullable: true),
                    paises_exporta = table.Column<string>(type: "text", nullable: true),
                    contrato_gobierno = table.Column<bool>(type: "boolean", nullable: false),
                    zona_indigena = table.Column<bool>(type: "boolean", nullable: false),
                    fuente_financiamiento_id = table.Column<int>(type: "integer", nullable: false),
                    subfuente_financiamiento_id = table.Column<int>(type: "integer", nullable: true),
                    ingresos_brutos_anuales = table.Column<double>(type: "double precision", nullable: false),
                    fecha_ingresos_brutos = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ingresos_exportaciones = table.Column<double>(type: "double precision", nullable: true),
                    ganancias_perdidas_brutas = table.Column<double>(type: "double precision", nullable: true),
                    fecha_ganancias_perdidas_brutas = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    descripcion_producto_servicio = table.Column<string>(type: "text", nullable: false),
                    areas_a_dominar = table.Column<string>(type: "text", nullable: true),
                    instrucciones = table.Column<string>(type: "text", nullable: true),
                    motivacion = table.Column<string>(type: "text", nullable: true),
                    lugar_desarrollo_emprendimiento = table.Column<string>(type: "text", nullable: true),
                    obstaculos = table.Column<string>(type: "text", nullable: true),
                    fondo_concursable = table.Column<string>(type: "text", nullable: false),
                    estatus_inicial = table.Column<string>(type: "text", nullable: false),
                    estatus_actual = table.Column<string>(type: "text", nullable: false),
                    fecha_establecimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nombre_propietario_id = table.Column<int>(type: "integer", nullable: false),
                    genero_propietario = table.Column<string>(type: "text", nullable: false),
                    ha_solicitado_credito = table.Column<bool>(type: "boolean", nullable: false),
                    como_solicito_credito = table.Column<string>(type: "text", nullable: true),
                    porque_no_credito = table.Column<string>(type: "text", nullable: true),
                    usa_pago_electronico = table.Column<bool>(type: "boolean", nullable: false),
                    medios_pago = table.Column<string>(type: "text", nullable: true),
                    notas = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clientes_empresas", x => x.id);
                    table.ForeignKey(
                        name: "FK_clientes_empresas_fuentes_financiamiento_fuente_financiamie~",
                        column: x => x.fuente_financiamiento_id,
                        principalTable: "fuentes_financiamiento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clientes_empresas_fuentes_financiamiento_subfuente_financia~",
                        column: x => x.subfuente_financiamiento_id,
                        principalTable: "fuentes_financiamiento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_niveles_formalizacion_nivel_formalizacion~",
                        column: x => x.nivel_formalizacion_id,
                        principalTable: "niveles_formalizacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_servicios_solicitados_servicio_solicitado~",
                        column: x => x.servicio_solicitado_id,
                        principalTable: "servicios_solicitados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tamano_empresas_tamano_empresa_id",
                        column: x => x.tamano_empresa_id,
                        principalTable: "tamano_empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tipos_cliente_nivel_tipo_cliente_nivel_id",
                        column: x => x.tipo_cliente_nivel_id,
                        principalTable: "tipos_cliente_nivel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tipos_clientes_estado_tipo_cliente_estado~",
                        column: x => x.tipo_cliente_estado_id,
                        principalTable: "tipos_clientes_estado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tipos_comercio_internacional_comercio_int~",
                        column: x => x.comercio_internacional_id,
                        principalTable: "tipos_comercio_internacional",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tipos_contabilidad_tipo_contabilidad_id",
                        column: x => x.tipo_contabilidad_id,
                        principalTable: "tipos_contabilidad",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tipos_empresa_tipo_empresa_id",
                        column: x => x.tipo_empresa_id,
                        principalTable: "tipos_empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clientes_empresas_tipos_organizacion_tipo_organizacion_id",
                        column: x => x.tipo_organizacion_id,
                        principalTable: "tipos_organizacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dni = table.Column<string>(type: "text", nullable: false),
                    nacionalidad = table.Column<string>(type: "text", nullable: false),
                    genero = table.Column<string>(type: "text", nullable: false),
                    telefono = table.Column<int>(type: "integer", nullable: false),
                    correo = table.Column<string>(type: "text", nullable: false),
                    rtn = table.Column<string>(type: "text", nullable: false),
                    direccion = table.Column<string>(type: "text", nullable: false),
                    ciudad = table.Column<string>(type: "text", nullable: false),
                    departamento = table.Column<string>(type: "text", nullable: false),
                    cargo = table.Column<string>(type: "text", nullable: false),
                    estado_civil_id = table.Column<int>(type: "integer", nullable: true),
                    nivel_estudio_id = table.Column<int>(type: "integer", nullable: false),
                    categoria_laboral_id = table.Column<int>(type: "integer", nullable: true),
                    posee_negocio = table.Column<bool>(type: "boolean", nullable: false),
                    nombre_etnia = table.Column<string>(type: "text", nullable: true),
                    localidad_etnica = table.Column<string>(type: "text", nullable: true),
                    contacto_discapacidad = table.Column<int>(type: "integer", nullable: true),
                    integrantes_totales_familia = table.Column<int>(type: "integer", nullable: true),
                    numero_hijos = table.Column<int>(type: "integer", nullable: true),
                    numero_hijas = table.Column<int>(type: "integer", nullable: true),
                    rol_contacto_familiar = table.Column<string>(type: "text", nullable: true),
                    centro = table.Column<string>(type: "text", nullable: true),
                    notas = table.Column<string>(type: "text", nullable: true),
                    empresa_cliente_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contactos", x => x.id);
                    table.ForeignKey(
                        name: "FK_contactos_clientes_empresas_empresa_cliente_id",
                        column: x => x.empresa_cliente_id,
                        principalTable: "clientes_empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contactos_categorias_laborales_categoria_laboral_id",
                        column: x => x.categoria_laboral_id,
                        principalTable: "categorias_laborales",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_contactos_estados_civiles_estado_civil_id",
                        column: x => x.estado_civil_id,
                        principalTable: "estados_civiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_contactos_niveles_estudio_nivel_estudio_id",
                        column: x => x.nivel_estudio_id,
                        principalTable: "niveles_estudio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sesiones_participantes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sesion_id = table.Column<int>(type: "integer", nullable: false),
                    contacto_id = table.Column<int>(type: "integer", nullable: false),
                    cliente_empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sesiones_participantes", x => x.id);
                    table.ForeignKey(
                        name: "fk_sesiones_participantes_clientes_empresas_cliente_empresa_id",
                        column: x => x.cliente_empresa_id,
                        principalTable: "clientes_empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sesiones_participantes_contactos_contacto_id",
                        column: x => x.contacto_id,
                        principalTable: "contactos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sesiones_participantes_sesiones_sesion_id",
                        column: x => x.sesion_id,
                        principalTable: "sesiones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_area_asesoria_id",
                table: "asesorias",
                column: "area_asesoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_cliente_id",
                table: "asesorias",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_fuente_financiamiento_id",
                table: "asesorias",
                column: "fuente_financiamiento_id");

            migrationBuilder.CreateIndex(
                name: "IX_asesorias_tipo_contacto_id",
                table: "asesorias",
                column: "tipo_contacto_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_archivos_asesoria_id",
                table: "asesorias_archivos",
                column: "asesoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_asesores_asesor_id",
                table: "asesorias_asesores",
                column: "asesor_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_asesores_asesoria_id",
                table: "asesorias_asesores",
                column: "asesoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_contactos_asesoria_id",
                table: "asesorias_contactos",
                column: "asesoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_contactos_cliente_empresa_id",
                table: "asesorias_contactos",
                column: "cliente_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetroleclaims_roleid",
                table: "aspnetroleclaims",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "rolenameindex",
                table: "aspnetroles",
                column: "normalizedname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserclaims_userid",
                table: "aspnetuserclaims",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserlogins_userid",
                table: "aspnetuserlogins",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserroles_roleid",
                table: "aspnetuserroles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "emailindex",
                table: "aspnetusers",
                column: "normalizedemail");

            migrationBuilder.CreateIndex(
                name: "usernameindex",
                table: "aspnetusers",
                column: "normalizedusername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_formato_programa_id",
                table: "capacitaciones",
                column: "formato_programa_id");

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_fuente_financiamiento_id",
                table: "capacitaciones",
                column: "fuente_financiamiento_id");

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_tema_principal_id",
                table: "capacitaciones",
                column: "tema_principal_id");

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_tipo_id",
                table: "capacitaciones",
                column: "tipo_id");

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_archivos_capacitacion_id",
                table: "capacitaciones_archivos",
                column: "capacitacion_id");

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_temas_capacitacion_id",
                table: "capacitaciones_temas",
                column: "capacitacion_id");

            migrationBuilder.CreateIndex(
                name: "ix_capacitaciones_temas_tema_id",
                table: "capacitaciones_temas",
                column: "tema_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_comercio_internacional_id",
                table: "clientes_empresas",
                column: "comercio_internacional_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresas_contacto_primario_id",
                table: "clientes_empresas",
                column: "contacto_primario_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresas_fuente_financiamiento_id",
                table: "clientes_empresas",
                column: "fuente_financiamiento_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_nivel_formalizacion_id",
                table: "clientes_empresas",
                column: "nivel_formalizacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresas_nombre_propietario_id",
                table: "clientes_empresas",
                column: "nombre_propietario_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_servicio_solicitado_id",
                table: "clientes_empresas",
                column: "servicio_solicitado_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresas_subfuente_financiamiento_id",
                table: "clientes_empresas",
                column: "subfuente_financiamiento_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_tamano_empresa_id",
                table: "clientes_empresas",
                column: "tamano_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_tipo_cliente_estado_id",
                table: "clientes_empresas",
                column: "tipo_cliente_estado_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_tipo_cliente_nivel_id",
                table: "clientes_empresas",
                column: "tipo_cliente_nivel_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_tipo_contabilidad_id",
                table: "clientes_empresas",
                column: "tipo_contabilidad_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_tipo_empresa_id",
                table: "clientes_empresas",
                column: "tipo_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_tipo_organizacion_id",
                table: "clientes_empresas",
                column: "tipo_organizacion_id");

            migrationBuilder.CreateIndex(
                name: "ix_contactos_categoria_laboral_id",
                table: "contactos",
                column: "categoria_laboral_id");

            migrationBuilder.CreateIndex(
                name: "IX_contactos_empresa_cliente_id",
                table: "contactos",
                column: "empresa_cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_contactos_estado_civil_id",
                table: "contactos",
                column: "estado_civil_id");

            migrationBuilder.CreateIndex(
                name: "ix_contactos_nivel_estudio_id",
                table: "contactos",
                column: "nivel_estudio_id");

            migrationBuilder.CreateIndex(
                name: "ix_cuotassesiones_sesionesid",
                table: "cuotassesiones",
                column: "sesionesid");

            migrationBuilder.CreateIndex(
                name: "ix_sesiones_capacitacion_capacitacion_id",
                table: "sesiones_capacitacion",
                column: "capacitacion_id");

            migrationBuilder.CreateIndex(
                name: "ix_sesiones_capacitacion_sesion_id",
                table: "sesiones_capacitacion",
                column: "sesion_id");

            migrationBuilder.CreateIndex(
                name: "ix_sesiones_participantes_cliente_empresa_id",
                table: "sesiones_participantes",
                column: "cliente_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_sesiones_participantes_contacto_id",
                table: "sesiones_participantes",
                column: "contacto_id");

            migrationBuilder.CreateIndex(
                name: "ix_sesiones_participantes_sesion_id",
                table: "sesiones_participantes",
                column: "sesion_id");

            migrationBuilder.AddForeignKey(
                name: "fk_asesorias_clientes_empresas_cliente_id",
                table: "asesorias",
                column: "cliente_id",
                principalTable: "clientes_empresas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asesorias_contactos_clientes_empresas_cliente_empresa_id",
                table: "asesorias_contactos",
                column: "cliente_empresa_id",
                principalTable: "clientes_empresas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_empresas_contactos_contacto_primario_id",
                table: "clientes_empresas",
                column: "contacto_primario_id",
                principalTable: "contactos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_empresas_contactos_nombre_propietario_id",
                table: "clientes_empresas",
                column: "nombre_propietario_id",
                principalTable: "contactos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contactos_clientes_empresas_empresa_cliente_id",
                table: "contactos");

            migrationBuilder.DropTable(
                name: "asesores_clientes_empresas");

            migrationBuilder.DropTable(
                name: "asesorias_archivos");

            migrationBuilder.DropTable(
                name: "asesorias_asesores");

            migrationBuilder.DropTable(
                name: "asesorias_contactos");

            migrationBuilder.DropTable(
                name: "aspnetroleclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserlogins");

            migrationBuilder.DropTable(
                name: "aspnetuserroles");

            migrationBuilder.DropTable(
                name: "aspnetusertokens");

            migrationBuilder.DropTable(
                name: "capacitaciones_archivos");

            migrationBuilder.DropTable(
                name: "capacitaciones_temas");

            migrationBuilder.DropTable(
                name: "cuotassesiones");

            migrationBuilder.DropTable(
                name: "sesiones_capacitacion");

            migrationBuilder.DropTable(
                name: "sesiones_participantes");

            migrationBuilder.DropTable(
                name: "asesorias");

            migrationBuilder.DropTable(
                name: "aspnetroles");

            migrationBuilder.DropTable(
                name: "aspnetusers");

            migrationBuilder.DropTable(
                name: "cuotas");

            migrationBuilder.DropTable(
                name: "capacitaciones");

            migrationBuilder.DropTable(
                name: "sesiones");

            migrationBuilder.DropTable(
                name: "areas_asesorias");

            migrationBuilder.DropTable(
                name: "tipos_contacto");

            migrationBuilder.DropTable(
                name: "formatos_programa");

            migrationBuilder.DropTable(
                name: "temas");

            migrationBuilder.DropTable(
                name: "tipos");

            migrationBuilder.DropTable(
                name: "clientes_empresas");

            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "fuentes_financiamiento");

            migrationBuilder.DropTable(
                name: "niveles_formalizacion");

            migrationBuilder.DropTable(
                name: "servicios_solicitados");

            migrationBuilder.DropTable(
                name: "tamano_empresas");

            migrationBuilder.DropTable(
                name: "tipos_cliente_nivel");

            migrationBuilder.DropTable(
                name: "tipos_clientes_estado");

            migrationBuilder.DropTable(
                name: "tipos_comercio_internacional");

            migrationBuilder.DropTable(
                name: "tipos_contabilidad");

            migrationBuilder.DropTable(
                name: "tipos_empresa");

            migrationBuilder.DropTable(
                name: "tipos_organizacion");

            migrationBuilder.DropTable(
                name: "categorias_laborales");

            migrationBuilder.DropTable(
                name: "estados_civiles");

            migrationBuilder.DropTable(
                name: "niveles_estudio");
        }
    }
}
