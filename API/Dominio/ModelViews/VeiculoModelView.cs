using System.Text.Json.Serialization;

namespace MinimalApi_CadastroCarros.Dominio.ModelViews;

public class VeiculoModelView
{
    [JsonPropertyName("id")]
    public int Id { get; set; } = default!;

    [JsonPropertyName("modelo")]
    public string Modelo { get; set; } = default!;

    [JsonPropertyName("marca")]
    public string Marca { get; set; } = default!;

    [JsonPropertyName("ano")]
    public int Ano { get; set; } = default!;
}
