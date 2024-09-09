using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using  MinimalApi_CadastroCarros.Dominio.Entidades;

namespace MinimalApi_CadastroCarros.Dominio.Utils;

public class GeradorToken
{

    public string GerarTokenJwt(Administrador administrador, string key)
    {
        var symmetricSecurityKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>(){
                           new Claim("Email", administrador.Email),
                           new Claim("Perfil", administrador.Perfil),
                           new Claim(ClaimTypes.Role, administrador.Perfil)
                         };


        var token = new JwtSecurityToken(
                            claims: claims,
                            expires : DateTime.Now.AddDays(1),
                            signingCredentials : credentials
                         );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}