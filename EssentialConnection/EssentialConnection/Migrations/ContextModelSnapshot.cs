﻿// <auto-generated />
using System;
using EssentialConnection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EssentialConnection.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AlunoVaga", b =>
                {
                    b.Property<int>("AlunosAlunoID")
                        .HasColumnType("int");

                    b.Property<int>("VagasVagaID")
                        .HasColumnType("int");

                    b.HasKey("AlunosAlunoID", "VagasVagaID");

                    b.HasIndex("VagasVagaID");

                    b.ToTable("AlunoVaga");
                });

            modelBuilder.Entity("EssentialConnection.Models.Aluno", b =>
                {
                    b.Property<int>("AlunoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlunoID"), 1L, 1);

                    b.Property<int?>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<int?>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlunoID");

                    b.HasIndex("CursoId");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("EssentialConnection.Models.Compentencias", b =>
                {
                    b.Property<int>("CompentenciasID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompentenciasID"), 1L, 1);

                    b.Property<int?>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompentenciasID");

                    b.HasIndex("CurriculoId");

                    b.ToTable("Compentencia");
                });

            modelBuilder.Entity("EssentialConnection.Models.Curriculo", b =>
                {
                    b.Property<int>("CurriculoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurriculoID"), 1L, 1);

                    b.Property<int?>("AlunoId")
                        .HasColumnType("int");

                    b.Property<string>("DescricaoPessoal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CurriculoID");

                    b.HasIndex("AlunoId")
                        .IsUnique()
                        .HasFilter("[AlunoId] IS NOT NULL");

                    b.ToTable("Curriculo");
                });

            modelBuilder.Entity("EssentialConnection.Models.Curso", b =>
                {
                    b.Property<int>("CursoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CursoID"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CursoID");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("EssentialConnection.Models.Empresa", b =>
                {
                    b.Property<int>("EmpresaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpresaID"), 1L, 1);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeResponsavel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpresaID");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("EssentialConnection.Models.ItensCurriculo", b =>
                {
                    b.Property<int>("ItensCurriculoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItensCurriculoID"), 1L, 1);

                    b.Property<int?>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instituicao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItensCurriculoID");

                    b.HasIndex("CurriculoId");

                    b.ToTable("ItensCurriculo");
                });

            modelBuilder.Entity("EssentialConnection.Models.Tinder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlunoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VagaId")
                        .HasColumnType("int");

                    b.Property<string>("nomeVaga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tinders");
                });

            modelBuilder.Entity("EssentialConnection.Models.TinderEmpresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<string>("EmpresaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nomeAluno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TinderEmpresa");
                });

            modelBuilder.Entity("EssentialConnection.Models.Vaga", b =>
                {
                    b.Property<int>("VagaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VagaID"), 1L, 1);

                    b.Property<int?>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataExpiracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("Responsavel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VagaID");

                    b.HasIndex("CursoId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Vaga");
                });

            modelBuilder.Entity("AlunoVaga", b =>
                {
                    b.HasOne("EssentialConnection.Models.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunosAlunoID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EssentialConnection.Models.Vaga", null)
                        .WithMany()
                        .HasForeignKey("VagasVagaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EssentialConnection.Models.Aluno", b =>
                {
                    b.HasOne("EssentialConnection.Models.Curso", "Curso")
                        .WithMany("Alunos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("EssentialConnection.Models.Compentencias", b =>
                {
                    b.HasOne("EssentialConnection.Models.Curriculo", "Curriculo")
                        .WithMany("Compentencias")
                        .HasForeignKey("CurriculoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Curriculo");
                });

            modelBuilder.Entity("EssentialConnection.Models.Curriculo", b =>
                {
                    b.HasOne("EssentialConnection.Models.Aluno", "Aluno")
                        .WithOne("Curriculo")
                        .HasForeignKey("EssentialConnection.Models.Curriculo", "AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Aluno");
                });

            modelBuilder.Entity("EssentialConnection.Models.ItensCurriculo", b =>
                {
                    b.HasOne("EssentialConnection.Models.Curriculo", "Curriculo")
                        .WithMany("ItensCurriculo")
                        .HasForeignKey("CurriculoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Curriculo");
                });

            modelBuilder.Entity("EssentialConnection.Models.Vaga", b =>
                {
                    b.HasOne("EssentialConnection.Models.Curso", "Curso")
                        .WithMany("Vagas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EssentialConnection.Models.Empresa", "Empresa")
                        .WithMany("Vagas")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Curso");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("EssentialConnection.Models.Aluno", b =>
                {
                    b.Navigation("Curriculo")
                        .IsRequired();
                });

            modelBuilder.Entity("EssentialConnection.Models.Curriculo", b =>
                {
                    b.Navigation("Compentencias");

                    b.Navigation("ItensCurriculo");
                });

            modelBuilder.Entity("EssentialConnection.Models.Curso", b =>
                {
                    b.Navigation("Alunos");

                    b.Navigation("Vagas");
                });

            modelBuilder.Entity("EssentialConnection.Models.Empresa", b =>
                {
                    b.Navigation("Vagas");
                });
#pragma warning restore 612, 618
        }
    }
}
