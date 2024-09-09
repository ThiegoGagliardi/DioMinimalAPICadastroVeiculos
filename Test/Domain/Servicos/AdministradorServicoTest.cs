using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Servico;
using MinimalApi_CadastroCarros.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class AdministradorServicoTest
{

    [TestMethod]
    public void TestandoSalvarAdministrador()
    {

        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
        var administradorServico = new AdministradorServico(context);

        var administrador = new Administrador();
        
        administrador.Email  = "teste@teste.com";
        administrador.Senha  = "senhateste";
        administrador.Perfil = "Adm";        

        //Act        
        administradorServico.Incluir(administrador);

        // Assert
        var administradorLocalizado = administradorServico.BuscarPorId(administrador.Id);
        
        Assert.AreEqual(administradorLocalizado.Id, administrador.Id);
        Assert.AreEqual(administradorLocalizado.Email, administrador.Email);
        Assert.AreEqual(administradorLocalizado.Senha, administrador.Senha);
        Assert.AreEqual(administradorLocalizado.Perfil, administrador.Perfil);
    }

    [TestMethod]
    public void BuscarPorIdTest()
    {
        // arrange
        var context = CriarContextoDeTeste();
        var administradorServico = new AdministradorServico(context);

        // act
        var administradorLocalizado = administradorServico.BuscarPorId(1);
        
        // assert
        Assert.AreEqual(administradorLocalizado.Id, 1);
    }

    [TestMethod]
    public void buscarTodosTeste()
    {
        // arrange
        var context = CriarContextoDeTeste();
        var administradorServico = new AdministradorServico(context);

        var administrador = new Administrador();        
        administrador.Email  = "teste@teste.com";
        administrador.Senha  = "senhateste";
        administrador.Perfil = "Editor";
        administradorServico.Incluir(administrador);        

        // act
        var administradores = administradorServico.BuscarTodos(1,10);

        // assert
        Assert.AreNotEqual(administradores.Count, 0);
    }

    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..","..",".."));      

        var builder = new ConfigurationBuilder()
                          .SetBasePath(path)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddEnvironmentVariables();

        var configuration = builder.Build();

        string connectionString = configuration.GetConnectionString("MySql");

        var options = new DbContextOptionsBuilder<DbContexto>()
                                                     .UseMySql(connectionString,  ServerVersion.AutoDetect(connectionString))
                                                     .Options;
        return new DbContexto(options);
    }

}