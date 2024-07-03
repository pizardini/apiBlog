using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
// [Authorize]
// [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
[ApiController]
public class ReactionController : ControllerBase
{
    private readonly DataContext context;

    public ReactionController(DataContext _context)
    {
        context = _context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reaction>>> Get()
    {
        try
        {
            return Ok(await context.Reactions.Include(p => p.NewsReaction).ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao listar reações");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Reaction item)
    {
        try
        {
            await context.Reactions.AddAsync(item);
            await context.SaveChangesAsync();
            return Ok("reação salva com sucesso!");
        }
        catch
        {
            return BadRequest("Erro ao salvar reação");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reaction>> Get([FromRoute] int id)
    {
        try
        {
            if (await context.Reactions.AnyAsync(p => p.Id == id))
                return Ok(await context.Reactions.Include(p => p.NewsReaction).FirstOrDefaultAsync(p => p.Id == id));
            else
                return NotFound("reação informada não encontrada");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca da reação");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Reaction model)
    {
        if (id != model.Id)
            return BadRequest("reação inválido");

        try
        {
            if (!await context.Reactions.AnyAsync(p => p.Id == id))
                return NotFound("reação não encontrado");

            context.Reactions.Update(model);
            await context.SaveChangesAsync();
            return Ok("reação salvo com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar reação");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Reaction model = await context.Reactions.FindAsync(id);

            if (model == null)
                return NotFound("reação inválido");

            context.Reactions.Remove(model);
            await context.SaveChangesAsync();
            return Ok("reação removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover notícia");
        }
    }
}