using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Entidades;

namespace MinimalApi_CadastroCarros.Dominio.Interface;

public interface IVeiculoServico
{
    List<Veiculo> BuscarTodos(int? pagina = 1, int? quantidade = 20, string? modelo = null, string? marca = null);

    Veiculo BuscarPorId (int id);

    void Atualizar (Veiculo veiculo);

    void Apagar(Veiculo veiculo);

    void Incluir(Veiculo veiculo);

}