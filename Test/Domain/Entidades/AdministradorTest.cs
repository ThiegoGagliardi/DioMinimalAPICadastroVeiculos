using MinimalApi_CadastroCarros.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adm = new Administrador();

        //Act
        adm.Id    = 1;
        adm.Email  = "teste@teste.com";
        adm.Senha  = "senhateste";
        adm.Perfil = "Adm";

        //Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("teste@teste.com", adm.Email);
        Assert.AreEqual("senhateste", adm.Senha);
        Assert.AreEqual("Adm", adm.Perfil);        
    }
}
