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

namespace Test.Request;

[TestClass]
public class VeiculosRequestTest
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
    public async Task TestarInserirVeiculoAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "adm@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
 
        var response  = await Setup.client.PostAsync("login", content);
              
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        VeiculoDTO veiculoDTO = new() { Ano = 2025, Marca = "marcateste", Modelo = "modeloteste"};

        content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado.Token);      

        // Act
        response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.Created,response.StatusCode);
    }

    [TestMethod]
    public async Task TestarLocalizarVeiculoAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "adm@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
 
        var response  = await Setup.client.PostAsync("login", content);
              
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        VeiculoDTO veiculoDTO = new() { Ano = 2025, Marca = "marcateste", Modelo = "modeloteste"};

        content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado.Token);      
        response = await Setup.client.PostAsync("/veiculos", content);
        result = await response.Content.ReadAsStringAsync();
        var veiculo =  JsonSerializer.Deserialize<VeiculoModelView>(result);
        
        // Act
         response = await Setup.client.GetAsync($"/veiculos/{veiculo.Id}");

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.OK,response.StatusCode);
    }

    [TestMethod]
    public async Task TestarDeletarVeiculoAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "adm@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
 
        var response  = await Setup.client.PostAsync("login", content);
              
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        VeiculoDTO veiculoDTO = new() { Ano = 2025, Marca = "marcateste", Modelo = "modeloteste"};

        content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado.Token);      
        response = await Setup.client.PostAsync("/veiculos", content);
        result = await response.Content.ReadAsStringAsync();
        var veiculo =  JsonSerializer.Deserialize<VeiculoModelView>(result);
        
        // Act
         response = await Setup.client.DeleteAsync($"/veiculos/{veiculo.Id}");

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.NoContent,response.StatusCode);
    }

    [TestMethod]
    public async Task TestarDeletarVeiculoForbiddenAsync()
    {
        //  Arrange
        LoginDTO loginDTO = new (){ Email = "editor@teste.com", Senha = "1234"};

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
 
        var response  = await Setup.client.PostAsync("login", content);
              
        var result = await response.Content.ReadAsStringAsync();
        var admLogado =  JsonSerializer.Deserialize<AdministradorLogadoModelView>(result);

        VeiculoDTO veiculoDTO = new() { Ano = 2025, Marca = "marcateste", Modelo = "modeloteste"};

        content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
        Setup.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado.Token);       
        
        // Act
        response = await Setup.client.DeleteAsync($"/veiculos/1");

        // Assert
        Assert.AreEqual<HttpStatusCode>(HttpStatusCode.Forbidden,response.StatusCode);
    }                  

}