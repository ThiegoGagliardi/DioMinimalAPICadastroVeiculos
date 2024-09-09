using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Interface;

namespace Test.Mocks;
public class VeiculosServicoMock : IVeiculoServico
{

    private static List<Veiculo> _veiculos = new List<Veiculo>() {
        new Veiculo() { Id = 1, Ano = 1990, Marca = "vw",  Modelo="fusca" }        
    };

    public void Apagar(Veiculo veiculo)    
    {
        var veiculoLocalizado = _veiculos.FirstOrDefault( v => v.Id == veiculo.Id);
        if(veiculoLocalizado != null){
           _veiculos.Remove(veiculoLocalizado);
        }        
    }

    public void Atualizar(Veiculo veiculo)
    {
         var veiculoLocalizado = _veiculos.FirstOrDefault( v => v.Id == veiculo.Id);
         veiculoLocalizado.Ano = veiculo.Ano;
         veiculoLocalizado.Modelo = veiculo.Modelo;
         veiculoLocalizado.Marca = veiculo.Marca;    
    }

    public Veiculo BuscarPorId(int id)
    {
        return _veiculos.FirstOrDefault( v => v.Id == id);
    }

    public List<Veiculo> BuscarTodos(int? pagina = 1, int? quantidade = 20, string? modelo = null, string? marca = null)
    {
        pagina    = pagina??1;
        quantidade = quantidade??10;

        return _veiculos.Skip((int)pagina* (int)quantidade).Take((int)quantidade).ToList();
    }

    public void Incluir(Veiculo veiculo)
    {
        veiculo.Id = _veiculos.Count+1;
        _veiculos.Add(veiculo);
    }
}