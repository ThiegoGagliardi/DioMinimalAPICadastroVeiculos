namespace MinimalApi_CadastroCarros.Dominio.DTOs;

public record LoginDTO
{
    public string Email { get; set;} = default!;
    public string Senha { get; set;} = default!;

}