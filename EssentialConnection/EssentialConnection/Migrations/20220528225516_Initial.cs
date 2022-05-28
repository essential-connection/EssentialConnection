using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EssentialConnection.Migrations
{
    public partial class Initial : Migration
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
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.EmpresaID);
                });

            migrationBuilder.CreateTable(
                name: "TinderEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurriculoId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nomeAluno = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinderEmpresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tinders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VagaId = table.Column<int>(type: "int", nullable: false),
                    nomeVaga = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    AlunoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: true),
                    CurriculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.AlunoID);
                    table.ForeignKey(
                        name: "FK_Aluno_Curso_CursoId",
                        column: x => x.CursoId,
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
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    CursoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.VagaID);
                    table.ForeignKey(
                        name: "FK_Vaga_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaga_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculo",
                columns: table => new
                {
                    CurriculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoPessoal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo", x => x.CurriculoID);
                    table.ForeignKey(
                        name: "FK_Curriculo_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlunoVaga",
                columns: table => new
                {
                    AlunosAlunoID = table.Column<int>(type: "int", nullable: false),
                    VagasVagaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoVaga", x => new { x.AlunosAlunoID, x.VagasVagaID });
                    table.ForeignKey(
                        name: "FK_AlunoVaga_Aluno_AlunosAlunoID",
                        column: x => x.AlunosAlunoID,
                        principalTable: "Aluno",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoVaga_Vaga_VagasVagaID",
                        column: x => x.VagasVagaID,
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
                    CurriculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compentencia", x => x.CompentenciasID);
                    table.ForeignKey(
                        name: "FK_Compentencia_Curriculo_CurriculoId",
                        column: x => x.CurriculoId,
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
                    CurriculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCurriculo", x => x.ItensCurriculoID);
                    table.ForeignKey(
                        name: "FK_ItensCurriculo_Curriculo_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculo",
                        principalColumn: "CurriculoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_CursoId",
                table: "Aluno",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoVaga_VagasVagaID",
                table: "AlunoVaga",
                column: "VagasVagaID");

            migrationBuilder.CreateIndex(
                name: "IX_Compentencia_CurriculoId",
                table: "Compentencia",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_AlunoId",
                table: "Curriculo",
                column: "AlunoId",
                unique: true,
                filter: "[AlunoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCurriculo_CurriculoId",
                table: "ItensCurriculo",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_CursoId",
                table: "Vaga",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_EmpresaId",
                table: "Vaga",
                column: "EmpresaId");
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
                name: "TinderEmpresa");

            migrationBuilder.DropTable(
                name: "Tinders");

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
