using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly DataContext context;

    public AdminController(DataContext _context)
    {
        context = _context;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Admin item)
{
    try
    {
        item.Password = GetPassword(item);

        await context.Admins.AddAsync(item);
        await context.SaveChangesAsync();
        return Ok("Usuário salvo com sucesso");
    }
    catch
    {
        return BadRequest("Erro ao salvar o usuário informado");
    }
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
private static string GetPassword(Admin admin)
{
    if (admin == null || admin.Password == null || admin.Password.Trim() == "")
        throw new Exception();

    string retorno = admin.Password;

    retorno = "sdfgg5g5" + retorno;
    retorno = Hash(retorno);
    retorno += "w54gw4545445";
    retorno = Hash(retorno);

    return retorno;
}

[HttpPost("autenticar")]
public async Task<ActionResult> Autenticar([FromBody] Admin item)
{
    try
    {
        Admin? existe = await context.Admins.FirstOrDefaultAsync(x => x.Email == item.Email);
        if (existe == null)
            return BadRequest("E-mail e/ou senha inválido(s)");

        item.Password = GetPassword(item);

        if (item.Password != existe.Password)
            return BadRequest("E-mail e/ou senha inválido(s)");

        existe.Password = "";
        return Ok(existe);
    }
    catch
    {
        return BadRequest("Erro geral");
    }
}
}