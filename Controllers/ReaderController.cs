    using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[Authorize]
// [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
[ApiController]
public class ReaderController : ControllerBase
{
    private readonly DataContext context;

    public ReaderController(DataContext _context)
    {
        context = _context;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody] Reader model) {
        try {
            if (await context.Readers.AnyAsync(p => p.Email == model.Email))
            return BadRequest("Já existe usuário com o e-mail informado");

            model.Password = GetPassword(model);
            await context.Readers.AddAsync(model);
            await context.SaveChangesAsync();
            return Ok("Usuário salco com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao inserir usuário informado");
        }
    }

    [NonAction]
    private static string Hash(string password) {
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
    private static string GetPassword(Reader reader) {
        if (reader == null || reader.Password == null || reader.Password.Trim() == "") {
            throw new Exception();
        }
        string reply = reader.Password;

        reply = "skdfjjhslkjf" + reply;
        reply = Hash(reply);
        reply = reply + "skdfhsjkf";
        reply = Hash(reply);

        return reply;
    }

}