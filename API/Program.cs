using MinimalApi_CadastroCarros;

IHostBuilder CreateHostBuilder(string [] args){

  return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults
                      (webBuilder => webBuilder.UseStartup<Startup>());

}

CreateHostBuilder(args).Build().Run();
/*
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text;

using MinimalApi_CadastroCarros.Infraestrutura.Db;
using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Interface;
using MinimalApi_CadastroCarros.Dominio.Servico;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.ModelViews;
using MinimalApi_CadastroCarros.Dominio.Enums;
using MinimalApi_CadastroCarros.Dominio.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;


#region Builder
var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetSection("Jwt").ToString();

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
                                  ).AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                  {
                                    ValidateLifetime = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                  });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Insert JWT Token"
  });
  options.AddSecurityRequirement(new OpenApiSecurityRequirement{{
                                                                    new OpenApiSecurityScheme{

                                                                     Reference = new OpenApiReference{
                                                                     Type = ReferenceType.SecurityScheme,
                                                                     Id = "Bearer"
                                                                     }

                                                                    },
                                                                    new string[] {}
}});
}
);

string connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<DbContexto>(options =>
{
  options.UseMySql(connectionString,
                                                      ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

var app = builder.Build();
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administradores
app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico AdministradorServico) =>
{
  var administrador = AdministradorServico.Login(loginDTO);

  if (administrador is null)
  {
    return Results.NotFound();
  }

  var token = new GeradorToken().GerarTokenJwt(administrador, key);
  return Results.Ok(new AdministradorLogadoModelView
  {
    Id = administrador.Id,
    Email = administrador.Email,
    Perfil = administrador.Perfil,
    Token = token
  });
}).AllowAnonymous().WithTags("Administradores");

app.MapPost("/administrador", ([FromBody] AdministadorDTO administradorDTO, IAdministradorServico administradorServico) =>
{

  var valida = ValidaAdministradorDTO(administradorDTO);

  if (valida != null)
  {
    return Results.BadRequest(valida);
  }

  Administrador administrador = new Administrador()
  {

    Email = administradorDTO.Email,
    Senha = administradorDTO.Senha,
    Perfil = administradorDTO.Perfil.ToString() ?? Perfil.Editor.ToString()
  };

  administradorServico.Incluir(administrador);

  return Results.Ok("");
}
).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm"}).WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, [FromQuery] int? quantidade, IAdministradorServico administradorServico) =>
{

  var administradores = administradorServico.BuscarTodos(pagina, quantidade);

  List<AdministradorModelView> administradoresModelView = new();

  foreach (var a in administradores)
  {

    administradoresModelView.Add(new AdministradorModelView(a));
  }

  return Results.Ok(administradoresModelView);
}
).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm"}).WithTags("Administradores");

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
{

  var administrador = administradorServico.BuscarPorId(id);

  if (administrador is null)
  {
    return Results.NotFound();
  }

  var administradorModelView = new AdministradorModelView(administrador);

  return Results.Ok(administradorModelView);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm"}).WithTags("Administradores");

ErrosDeValidacao ValidaAdministradorDTO(AdministadorDTO administadorDTO)
{

  var validacao = new ErrosDeValidacao();

  if (administadorDTO.Perfil is null)
  {
    validacao.Mensagens.Add("O perfil precisa estar preenchido.");
  }

  if (string.IsNullOrEmpty(administadorDTO.Senha))
  {
    validacao.Mensagens.Add("A senha precisa estar preenchido.");
  }

  if (string.IsNullOrEmpty(administadorDTO.Email))
  {
    validacao.Mensagens.Add("O Email precisa estar preenchido.");
  }

  if (validacao.Mensagens.Count > 0)
  {
    return validacao;
  }

  return null;
}

#endregion

#region Veiculos

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{

  var validacao = ValidaVeiculoDTO(veiculoDTO);

  if (validacao != null)
  {
    return Results.BadRequest(validacao);
  }

  var veiculo = new Veiculo
  {
    Modelo = veiculoDTO.Modelo,
    Marca = veiculoDTO.Marca,
    Ano = veiculoDTO.Ano,
  };

  veiculoServico.Incluir(veiculo);

  return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm"}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, [FromQuery] int? quantidade, IVeiculoServico veiculoServico) =>
{
  var veiculos = veiculoServico.BuscarTodos(pagina, quantidade);
  return Results.Ok(veiculos);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm, Editor"}).WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
  var veiculo = veiculoServico.BuscarPorId(id);

  if (veiculo is null)
  {
    return Results.NotFound("Veículo não localizado.");
  }

  return Results.Ok(veiculo);
}
).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm, Editor"}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", (int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
  var veiculo = veiculoServico.BuscarPorId(id);

  if (veiculo is null)
  {
    return Results.NotFound("Veículo não localizado para atualização");
  }

  var validacao = ValidaVeiculoDTO(veiculoDTO);

  if (validacao != null)
  {
    return Results.BadRequest(validacao);
  }

  veiculo.Marca = veiculoDTO.Marca;
  veiculo.Ano = veiculoDTO.Ano;
  veiculo.Modelo = veiculoDTO.Modelo;

  veiculoServico.Atualizar(veiculo);

  return Results.Ok(veiculo);
}
).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm"}).WithTags("Veiculos");


app.MapDelete("/veiculos/{id}", (int id, IVeiculoServico veiculoServico) =>
{
  var veiculo = veiculoServico.BuscarPorId(id);
  if (veiculo is null)
  {
    return Results.NotFound("Veículo não localizado para remoção");
  }

  veiculoServico.Apagar(veiculo);

  return Results.NoContent();
}
).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute{ Roles ="Adm"}).WithTags("Veiculos");

ErrosDeValidacao ValidaVeiculoDTO(VeiculoDTO veiculoDTO)
{

  var validacao = new ErrosDeValidacao();

  if (string.IsNullOrEmpty(veiculoDTO.Modelo))
  {
    validacao.Mensagens.Add("O modelo não pode ser vazio.");
  }

  if (string.IsNullOrEmpty(veiculoDTO.Marca))
  {
    validacao.Mensagens.Add("A marca não pode ser vazia.");
  }

  if (veiculoDTO.Ano < 1950)
  {
    validacao.Mensagens.Add("Ano muito antigo.Inferior a 1950");
  }

  if (validacao.Mensagens.Count > 0)
  {
    return validacao;
  }

  return null;
}

#endregion

app.Run();
*/