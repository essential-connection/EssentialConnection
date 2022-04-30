using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Atividade_Intregadora.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curriculo> Curriculos { get; set; }

        public DbSet<Curso> Curso { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Vagas> Vagas { get; set; }

        public DbSet<Competencias> Competencias { get; set; }
        
        public DbSet<ItensCurriculo> ItensCurriculos { get; set; }

    }
}

