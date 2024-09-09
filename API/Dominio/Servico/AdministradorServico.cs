using Microsoft.EntityFrameworkCore;
using MinimalApi_CadastroCarros.Dominio.DTOs;
using MinimalApi_CadastroCarros.Dominio.Entidades;
using MinimalApi_CadastroCarros.Dominio.Interface;
using MinimalApi_CadastroCarros.Infraestrutura.Db;

namespace MinimalApi_CadastroCarros.Dominio.Servico;

public class AdministradorServico : IAdministradorServico
{
    private DbContexto _context;

    public AdministradorServico(DbContexto context)
    {
        this._context = context;        
    }

    public Administrador? Login(LoginDTO loginDTO)
    {

        var admin = _context.Administradores.FirstOrDefault( a => 
                                                        a.Email == loginDTO.Email &&
                                                        a.Senha == loginDTO.Senha
                                                );
        
        return admin;
    }

    public Administrador Incluir(Administrador administrador){

        _context.Administradores.Add(administrador);
        _context.SaveChanges();

        return administrador;
    }

    public List<Administrador> BuscarTodos(int? pagina, int? quantidade){
        
        pagina     = pagina ?? 1;
        quantidade = quantidade ?? 20;     
       
        return _context.Administradores.Skip(((int)pagina - 1)* (int)quantidade).Take((int)quantidade).ToList();        
    }

    public Administrador BuscarPorId(int id) {

        var administrador = _context.Administradores.FirstOrDefault( a => a.Id == id);

        return administrador;
    }
}