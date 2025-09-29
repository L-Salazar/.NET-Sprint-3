using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eficientiza_s3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_mtt_estacao_c3",
                columns: table => new
                {
                    id_estacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_estacao = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ds_localizacao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    nr_capacidade = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ds_status = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    dt_ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ds_responsavel = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_mtt_estacao_c3", x => x.id_estacao);
                });

            migrationBuilder.CreateTable(
                name: "tb_mtt_moto_c3",
                columns: table => new
                {
                    id_moto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_placa = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    nm_modelo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ds_cor = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: true),
                    nr_ano = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ds_status = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_mtt_moto_c3", x => x.id_moto);
                });

            migrationBuilder.CreateTable(
                name: "tb_mtt_usuario_c3",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_usuario = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ds_senha = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    tp_usuario = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_mtt_usuario_c3", x => x.id_usuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_mtt_estacao_c3");

            migrationBuilder.DropTable(
                name: "tb_mtt_moto_c3");

            migrationBuilder.DropTable(
                name: "tb_mtt_usuario_c3");
        }
    }
}
