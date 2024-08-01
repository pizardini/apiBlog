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
public class ReaderController : ControllerBase
{
    private readonly DataContext context;

    public ReaderController(DataContext _context)
    {
        context = _context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Reader>>> Get()
    {
        try
        {
            return Ok(await context.Readers.ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao listar usuários");
        }
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody] Reader model) {
        try {
            if (await context.Readers.AnyAsync(p => p.Email == model.Email))
            return BadRequest("Já existe usuário com o e-mail informado");

            // model.Password = GetPassword(model);
            await context.Readers.AddAsync(model);
            await context.SaveChangesAsync();

            model.Type = 2;
            return Ok("Usuário salvo com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao inserir usuário informado");
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Reader>> Get([FromRoute] int id)
    {
        try
        {
            if (await context.Readers.AnyAsync(p => p.Id == id))
                return Ok(await context.Readers.FindAsync(id));
            else
                return NotFound("O usuário informado não foi encontrado");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca de usuário");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Reader model)
    {
        if (id != model.Id)
            return BadRequest("Usuário inválido");

        try
        {
            if (!await context.Readers.AnyAsync(p => p.Id == id))
                return NotFound("Usuário não encontrado");

            context.Readers.Update(model);
            await context.SaveChangesAsync();
            return Ok("Usuário salvo com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar usuário informado");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Reader model = await context.Readers.FindAsync(id);

            if (model == null)
                return NotFound("Usuário inválido");

            context.Readers.Remove(model);
            await context.SaveChangesAsync();
            Console.WriteLine("teste");
            return Ok("Usuário removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover o usuário");
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