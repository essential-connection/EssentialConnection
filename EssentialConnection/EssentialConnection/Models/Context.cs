using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace EssentialConnection.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Compentencias> Competencias { get; set; }
        public DbSet<Curriculo> Curriculos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<ItensCurriculo> ItensCurriculos { get; set; }

  //      Colocar em EnssencialConnection
  //          <ItemGroup>
		//<PackageReference Include = "Microsoft.EntityFrameworkCore" Version="5.0.14" />
		//<PackageReference Include = "Microsoft.EntityFrameworkCore.Design" Version="5.0.14">
		//	<PrivateAssets>all</PrivateAssets>
		//	<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		//</PackageReference>
		//<PackageReference Include = "Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.14" />
		//<PackageReference Include = "Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="5.0.2" />
		//<DotNetCliToolReference Include = "Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
	 //     </ItemGroup>

    }
}
