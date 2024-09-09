namespace MinimalApi_CadastroCarros.Dominio.Entidades;

public class Veiculo
{
    public int Id { get; set; } = default!;

    public string Modelo { get; set; } = default!;

    public string Marca { get; set; } = default!;

    public int Ano { get; set; } = default!;
}