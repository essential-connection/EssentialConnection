using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EssentialConnection.Migrations
{
    public partial class Inicialcriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoID);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.EmpresaID);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    AlunoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    CursoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.AlunoID);
                    table.ForeignKey(
                        name: "FK_Aluno_Curso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaga",
                columns: table => new
                {
                    VagaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    CursoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.VagaID);
                    table.ForeignKey(
                        name: "FK_Vaga_Curso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaga_Empresa_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresa",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculo",
                columns: table => new
                {
                    CurriculoID = table.Column<int>(type: "int", nullable: false),
                    DescricaoPessoal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo", x => x.CurriculoID);
                    table.ForeignKey(
                        name: "FK_Curriculo_Aluno_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Aluno",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlunoVaga",
                columns: table => new
                {
                    AlunosAlunoID = table.Column<int>(type: "int", nullable: false),
                    VagaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoVaga", x => new { x.AlunosAlunoID, x.VagaID });
                    table.ForeignKey(
                        name: "FK_AlunoVaga_Aluno_AlunosAlunoID",
                        column: x => x.AlunosAlunoID,
                        principalTable: "Aluno",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoVaga_Vaga_VagaID",
                        column: x => x.VagaID,
                        principalTable: "Vaga",
                        principalColumn: "VagaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compentencia",
                columns: table => new
                {
                    CompentenciasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurriculoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compentencia", x => x.CompentenciasID);
                    table.ForeignKey(
                        name: "FK_Compentencia_Curriculo_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Curriculo",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensCurriculo",
                columns: table => new
                {
                    ItensCurriculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instituicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurriculoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCurriculo", x => x.ItensCurriculoID);
                    table.ForeignKey(
                        name: "FK_ItensCurriculo_Curriculo_CurriculoID",
                        column: x => x.CurriculoID,
                        principalTable: "Curriculo",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_CursoID",
                table: "Aluno",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoVaga_VagaID",
                table: "AlunoVaga",
                column: "VagaID");

            migrationBuilder.CreateIndex(
                name: "IX_Compentencia_CurriculoID",
                table: "Compentencia",
                column: "CurriculoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCurriculo_CurriculoID",
                table: "ItensCurriculo",
                column: "CurriculoID");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_CursoID",
                table: "Vaga",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_EmpresaID",
                table: "Vaga",
                column: "EmpresaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoVaga");

            migrationBuilder.DropTable(
                name: "Compentencia");

            migrationBuilder.DropTable(
                name: "ItensCurriculo");

            migrationBuilder.DropTable(
                name: "Vaga");

            migrationBuilder.DropTable(
                name: "Curriculo");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Curso");
        }
    }
}
