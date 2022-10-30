using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(
        DbContextOptions<DatabaseContext> options
        ) : base(options)
    {

    }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Contrato> contratos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Pessoa>(tabela =>
        {
            tabela.HasKey(e => e.Id); //Gera chave primaria
            //Gera chave estrangeira (Uma pessoa pode ter muitos contratos)
            tabela
            .HasMany(e => e.contratos) //tem muitos
            .WithOne() //Com uma
            .HasForeignKey(c => c.PessoaId); //com a chave estrangeira
        });

        builder.Entity<Contrato>(tabela =>
        {
            tabela.HasKey(e => e.Id);
        });
    }
}