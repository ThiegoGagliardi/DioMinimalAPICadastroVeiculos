using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Servico;
using MinimalApi_CadastroCarros.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServicoTest
{
    [TestMethod]
    public void SalvarVeiculoTeste()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");
        var VeiculoServico = new VeiculoServico(context);

        var veiculo = new Veiculo();

        veiculo.Ano = 2010;
        veiculo.Modelo = "ModeloTeste";
        veiculo.Marca = "MarcaTeste";

        //Act        
        VeiculoServico.Incluir(veiculo);

        // Assert
        var VeiculoLocalizado = VeiculoServico.BuscarPorId(veiculo.Id);

        Assert.AreEqual(VeiculoLocalizado.Id, veiculo.Id);
        Assert.AreEqual(VeiculoLocalizado.Ano, veiculo.Ano);
        Assert.AreEqual(VeiculoLocalizado.Marca, veiculo.Marca);
        Assert.AreEqual(VeiculoLocalizado.Modelo, veiculo.Modelo);

    }

    [TestMethod]
    public void AtualizarVeiculoTeste()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");
        var VeiculoServico = new VeiculoServico(context);

        var veiculo = new Veiculo();

        veiculo.Ano = 2010;
        veiculo.Modelo = "ModeloTeste";
        veiculo.Marca = "MarcaTeste";

        VeiculoServico.Incluir(veiculo);

        veiculo.Ano = 2011;
        veiculo.Modelo = "ModeloTeste2";
        veiculo.Marca = "MarcaTeste2";

        //Act
        VeiculoServico.Atualizar(veiculo);

        // Assert
        var VeiculoLocalizado = VeiculoServico.BuscarPorId(veiculo.Id);

        Assert.AreEqual(VeiculoLocalizado.Id, veiculo.Id);
        Assert.AreEqual(VeiculoLocalizado.Ano, veiculo.Ano);
        Assert.AreEqual(VeiculoLocalizado.Marca, veiculo.Marca);
        Assert.AreEqual(VeiculoLocalizado.Modelo, veiculo.Modelo);

    }

    [TestMethod]
    public void BuscarPorIdTest()
    {
        // arrange
        var context = CriarContextoDeTeste();
        var VeiculoServico = new VeiculoServico(context);

        // act
        var VeiculoLocalizado = VeiculoServico.BuscarPorId(1);

        // assert
        Assert.AreEqual(VeiculoLocalizado.Id, 1);
    }

    [TestMethod]
    public void buscarTodosTeste()
    {
        // arrange
        var context = CriarContextoDeTeste();
        var VeiculoServico = new VeiculoServico(context);

        var veiculo = new Veiculo();
        veiculo.Ano = 2012;
        veiculo.Modelo = "ModeloTeste3";
        veiculo.Marca = "MarcaTeste3";

        VeiculoServico.Incluir(veiculo);      

        // act
        var veiculos = VeiculoServico.BuscarTodos(1,10);

        // assert
        Assert.AreNotEqual(veiculos.Count, 0);
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
                                                     .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                                                     .Options;
        return new DbContexto(options);
    }

}
