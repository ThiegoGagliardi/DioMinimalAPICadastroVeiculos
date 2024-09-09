using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Enums;

namespace MinimalApi_CadastroCarros.Dominio.ModelViews;

public class AdministradorModelView
{

    public int Id { get; set;} = default!;

    public string Email { get; set; } = default!;

    public string Perfil { get; set;} = default!;

    public AdministradorModelView(Administrador administrador)
    {
        this.Id   = administrador.Id;
        this.Email = administrador.Email;
        this.Perfil = administrador.Perfil;        
    }

}
