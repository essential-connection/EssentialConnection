using Microsoft.EntityFrameworkCore;

namespace EssentialConnection.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Elimina o bug de 1 para 1 do Curriculo e Aluno || NAO MEXER
            modelBuilder.Entity<Aluno>()
            .HasOne(a => a.Curriculo)
            .WithOne(a => a.Aluno)
            .HasForeignKey<Curriculo>(c => c.CurriculoID);

            //Corrigi o erro de N para M de alunos e vagas || Retirar Cascata || Se alguem achar outra solução, mudar
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Compentencias> Compentencia { get; set; }
        public DbSet<Curriculo> Curriculo  { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Empresa> Empresa  { get; set; }
        public DbSet<Vaga> Vaga  { get; set; }
        public DbSet<ItensCurriculo> ItensCurriculo  { get; set; }

    }
}
