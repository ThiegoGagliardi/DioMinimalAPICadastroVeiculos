using Microsoft.EntityFrameworkCore;
using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Interface;
using MinimalApi_CadastroCarros.Infraestrutura.Db;

namespace MinimalApi_CadastroCarros.Dominio.Servico;

public class VeiculoServico : IVeiculoServico
{    
    private readonly DbContexto _contexto;

    public VeiculoServico(DbContexto contexto){
        this._contexto = contexto;
    }

    public void Apagar(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }

    public void Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
    }

    public void Incluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();
    }    

    public Veiculo? BuscarPorId(int id)
    {
        var veiculo = _contexto.Veiculos.FirstOrDefault( v => v.Id == id);
        return veiculo;
    }

    public List<Veiculo> BuscarTodos(int? pagina = 1, int? quantidade = 20, string? modelo = null, string? marca = null)
    {
        var query = _contexto.Veiculos.AsQueryable();

        if (!String.IsNullOrEmpty(modelo)){
            query = query.Where(v => EF.Functions.Like(v.Modelo.ToLower(), $"%{modelo.ToLower()}%'"));
        }

        if (!String.IsNullOrEmpty(marca)){
            query = query.Where(v => EF.Functions.Like(v.Marca.ToLower(), $"%{marca.ToLower()}%'"));
        }

        pagina     = pagina ?? 1;
        quantidade = quantidade ?? 20;

        query = query.Skip(((int)pagina - 1)* (int)quantidade).Take((int)quantidade);

        return query.ToList();
    }
}
