using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Interface;

namespace Test.Mocks;

public class AdministradorServicoMock : IAdministradorServico
{
    private static List<Administrador> _administradores = new List<Administrador>() {
        new Administrador() { Id = 1, Email = "adm@teste.com", Senha = "1234", Perfil = "Adm" },
        new Administrador() { Id = 2, Email = "editor@teste.com", Senha = "1234", Perfil = "Editor" }
    };

    public Administrador BuscarPorId(int id)
    {
        return _administradores.Find( a => a.Id == id);
    }

    public List<Administrador> BuscarTodos(int? pagina, int? quantidade)
    {
        pagina     = pagina??1;
        quantidade = quantidade??20;

        return _administradores.Skip(((int)pagina - 1)* (int)quantidade).Take((int)quantidade).ToList();        
    }

    public Administrador Incluir(Administrador administrador)
    {
        administrador.Id = _administradores.Count + 1;
        _administradores.Add(administrador);

        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        return _administradores.Find(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
    }
}