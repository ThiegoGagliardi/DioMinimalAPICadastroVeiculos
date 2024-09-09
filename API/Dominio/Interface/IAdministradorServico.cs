using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Entidades;

namespace MinimalApi_CadastroCarros.Dominio.Interface;

public interface IAdministradorServico
{
    Administrador? Login(LoginDTO loginDTO);

    Administrador Incluir (Administrador administrador);

    List<Administrador> BuscarTodos(int? pagina, int? quantidade);

    Administrador BuscarPorId(int id);
}