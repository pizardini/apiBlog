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
public class CommentController : ControllerBase
{
    private readonly DataContext context;

    public CommentController(DataContext _context)
    {
        context = _context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> Get()
    {
        try
        {
            return Ok(await context.Comments.Include(p => p.NewsComment).ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao listar comentários");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Comment item)
    {
        try
        {
            await context.Comments.AddAsync(item);
            await context.SaveChangesAsync();
            return Ok("comentário salva com sucesso!");
        }
        catch
        {
            return BadRequest("Erro ao salvar comentário");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> Get([FromRoute] int id)
    {
        try
        {
            if (await context.Comments.AnyAsync(p => p.Id == id))
                return Ok(await context.Comments.Include(p => p.ReaderComment).FirstOrDefaultAsync(p => p.Id == id));
            else
                return NotFound("Comentário informada não encontrada");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca do comentário");
        }
    }

    [HttpGet("news/{id}")]
    public async Task<ActionResult<Comment>> GetFromNews([FromRoute] int id)
    {
        try
        {
            var comments = await context.Comments
                .Where(c => c.NewsId == id)
                .Include(c => c.ReaderComment) // Incluindo dados do leitor, se necessário
                .ToListAsync();

            if (comments == null || !comments.Any())
            {
                return NotFound("Nenhum comentário encontrado para a notícia informada.");
            }

            return Ok(comments);
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca do comentário");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Comment model)
    {
        if (id != model.Id)
            return BadRequest("Comentário inválido");

        try
        {
            if (!await context.Comments.AnyAsync(p => p.Id == id))
                return NotFound("Comentário não encontrado");

            context.Comments.Update(model);
            await context.SaveChangesAsync();
            return Ok("Comentário salvo com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar comentário");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Comment model = await context.Comments.FindAsync(id);

            if (model == null)
                return NotFound("Comentário inválido");

            context.Comments.Remove(model);
            await context.SaveChangesAsync();
            return Ok("Comentário removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover notícia");
        }
    }
}