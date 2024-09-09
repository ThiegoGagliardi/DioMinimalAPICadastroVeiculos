using MinimalApi_CadastroCarros.Dominio.Enums;

namespace MinimalApi_CadastroCarros.Dominio.DTOs;

public class AdministadorDTO
{
    private Perfil _perfil = Enums.Perfil.Editor;
    public string Email { get; set;} = default!;
    public string Senha { get; set;} = default!;
    public string? Perfil { get => _perfil.ToString(); 
                            set{
                                this._perfil = (Perfil)Enum.Parse(typeof(Perfil), value.ToString());
                            }}

}