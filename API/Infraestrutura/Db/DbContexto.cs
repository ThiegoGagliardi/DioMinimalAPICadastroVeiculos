using Microsoft.EntityFrameworkCore;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Enums;

namespace MinimalApi_CadastroCarros.Infraestrutura.Db;

public class DbContexto : DbContext
{

    public DbSet<Administrador> Administradores { get; set; }

    public DbSet<Veiculo> Veiculos { get; set;}

    public DbContexto(DbContextOptions<DbContexto> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(a => a.HasKey(a => a.Id));
        modelBuilder.Entity<Administrador>().Property(a => a.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Administrador>().Property(a => a.Senha).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Administrador>().Property(a => a.Senha).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Administrador>().Property(a => a.Perfil).HasMaxLength(10).IsRequired();

        modelBuilder.Entity<Veiculo>(v => v.HasKey(v => v.Id));
        modelBuilder.Entity<Veiculo>().Property(v => v.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Veiculo>().Property(v => v.Modelo).HasMaxLength(150).IsRequired();
        modelBuilder.Entity<Veiculo>().Property(v => v.Marca).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Veiculo>().Property(v => v.Ano).IsRequired();

        modelBuilder.Entity<Administrador>().HasData(
            new Administrador {
                               Id = 1,
                               Email = "admin@teste.com",
                               Senha = "1234",
                               Perfil = Perfil.Adm.ToString()}
        );
    }
}
