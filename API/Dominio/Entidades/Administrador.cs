using MinimalApi_CadastroCarros.Dominio.Enums;

namespace MinimalApi_CadastroCarros.Dominio.Entidades;

public class Administrador
{
    
    public int Id { get; set;} = default!;

    public string Email { get; set; } = default!;

    public string Senha { get; set; } = default!;

    public string Perfil { get; set;} = default!;

}