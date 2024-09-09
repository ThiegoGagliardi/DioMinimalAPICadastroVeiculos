using MinimalApi_CadastroCarros.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetVeiculo(){
        
        // Arragement
        Veiculo veiculo = new Veiculo();

        // Act
        veiculo.Id     = 1;
        veiculo.Modelo = "testeModelo";
        veiculo.Marca  = "testeMarca";
        veiculo.Ano    = 2010;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("testeModelo", veiculo.Modelo);
        Assert.AreEqual("testeMarca", veiculo.Marca);
        Assert.AreEqual(2010, veiculo.Ano);
    }
}
