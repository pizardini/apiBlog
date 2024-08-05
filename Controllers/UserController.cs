using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]

[ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DataContext context;

    public UserController(DataContext _context)
    {
        context = _context;
    }

[HttpGet]
public async Task<ActionResult<IEnumerable<User>>> Get()
{
    try
    {
        return Ok(await context.Users.ToListAsync());
    }
    catch
    {
        return BadRequest("Erro ao listar usuários");
    }
}

[HttpGet("obterporemail/{email}")]
public async Task<ActionResult> ObterPorEmail([FromRoute] string email)
{
    if (await context.Users.AnyAsync(p => p.Email == email))
    {
        User user = await context.Users.FirstOrDefaultAsync(p => p.Email == email);
        if (user != null)
            user.Password = "";
        return Ok(user);
    }
    else
        return BadRequest("Usuário não encontrado");
}

[HttpGet("exists")]
public async Task<ActionResult> CheckAdmin()
{
    if (await context.Users.AnyAsync(p => p.Type == 0))
    {
        
        return Ok(new { exist = true });
    }
    else
        return Ok(new { exist = false });
}

[NonAction]
private static string Hash(string password)
{
    HashAlgorithm hasher = HashAlgorithm.Create(HashAlgorithmName.SHA512.Name);
    byte[] stringBytes = Encoding.ASCII.GetBytes(password);
    byte[] byteArray = hasher.ComputeHash(stringBytes);

    StringBuilder stringBuilder = new StringBuilder();
    foreach (byte b in byteArray)
    {
        stringBuilder.AppendFormat("{0:x2}", b);
    }

    return stringBuilder.ToString();
}

[NonAction]
private static string GetPassword(User user)
{
    if (user == null || user.Password == null || user.Password.Trim() == "")
        throw new Exception();

    string retorno = user.Password;

    retorno = "sdfgg5g5" + retorno;
    retorno = Hash(retorno);
    retorno += "w54gw4545445";
    retorno = Hash(retorno);

    return retorno;
}

[HttpPost("autenticar")]
public async Task<ActionResult> Autenticar([FromBody] User model)
{
    try
    {
        User? existe = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (existe == null)
            return BadRequest("E-mail e/ou senha inválido(s)1");

        model.Password = GetPassword(model);
        if (model.Password != existe.Password)
            return BadRequest("E-mail e/ou senha inválido(s)2");

        if (model.Active) {
            return BadRequest("Usuário inativo ou bloqueado");
        }
        
        existe.Password = "";
        return Ok(existe);
    }
    catch
    {
        return BadRequest("Erro geral");
    }
}


[HttpPut("{id}")]
public async Task<ActionResult> Put([FromRoute] int id, [FromBody] User model)
{
    if (id != model.Id)
        return BadRequest("Usuário inválido");

    try
    {
        if (!await context.Users.AnyAsync(p => p.Id == id))
            return NotFound("Usuário não encontrado");

        context.Users.Update(model);
        await context.SaveChangesAsync();
        return Ok("Usuário salvo com sucesso");
    }
    catch
    {
        return BadRequest("Erro ao salvar usuário informado");
    }
}

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get([FromRoute] int id)
    {
        try
        {
            if (await context.Users.AnyAsync(p => p.Id == id))
                return Ok(await context.Users.FindAsync(id));
            else
                return NotFound("O usuário informado não foi encontrado");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca de usuário");
        }
    }
}