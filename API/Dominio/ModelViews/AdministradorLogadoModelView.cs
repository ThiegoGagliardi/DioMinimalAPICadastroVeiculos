using  System.Text.Json.Serialization;

namespace MinimalApi_CadastroCarros.Dominio.ModelViews;

public record AdministradorLogadoModelView
{
    [JsonPropertyName("id")]
    public int Id{ get; set;} = default!;

   [JsonPropertyName("email")]
    public string Email {get; set;} = default!;

    [JsonPropertyName("perfil")]
    public string Perfil {get; set;} = default!;

    [JsonPropertyName("token")]
    public string Token {get; set;} = default!;

}