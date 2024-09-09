using System.Net;
using System.Security;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.ModelViews;
using Test.Helpers;

namespace Test.Requests;


[TestClass]
public class AdministradorRequestTest
{

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
         Setup.ClassInit(context);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task TestarLoginAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "adm@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");

        // Act
        var response  = await Setup.client.PostAsync("login", content);

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.OK,response.StatusCode);
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        Assert.IsNotNull(admLogado?.Email ?? "");
        Assert.IsNotNull(admLogado?.Perfil ?? "");
        Assert.IsNotNull(admLogado?.Token ?? "");
    }

    [TestMethod]
    public async Task TestarInserirAdministradorAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "adm@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
 
        var response  = await Setup.client.PostAsync("login", content);
              
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        AdministadorDTO administadorDTO = new () { Email = "teste@teste.com", Senha = "45678",  Perfil="Adm"};

        content = new StringContent(JsonSerializer.Serialize(administadorDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado.Token);

        // Act
        response  = await Setup.client.PostAsync("/administrador", content);

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.Created,response.StatusCode);
    }

    [TestMethod]
    public async Task TestarInserirAdministradorForbiddenAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "editor@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
  
        var response  = await Setup.client.PostAsync("login", content);
        
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        AdministadorDTO administadorDTO = new () { Email = "teste@teste.com", Senha = "45678",  Perfil="Adm"};

        content = new StringContent(JsonSerializer.Serialize(administadorDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado.Token);
        
        // Act
        response  = await Setup.client.PostAsync("/administrador", content);

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.Forbidden,response.StatusCode);
    }


    [TestMethod]
    public async Task TestarInserirAdministradorUnauthorizedAsync()
    {
        //  Arrange        
        AdministadorDTO administadorDTO = new () { Email = "teste@teste.com", Senha = "45678",  Perfil="Adm"};

        var content = new StringContent(JsonSerializer.Serialize(administadorDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
        
        // Act
        var response  = await Setup.client.PostAsync("/administrador", content);

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.Unauthorized,response.StatusCode);
    }            
}