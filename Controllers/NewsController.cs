using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class NewsController : Controller
{
    private readonly DataContext context;

    public NewsController(DataContext _context)
    {
        context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> Get()
    {
        try
        {
            return Ok(await context.NewsItems.Include(p => p.UserNews).ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao listar as notícias");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] News item)
    {
        try
        {
            await context.NewsItems.AddAsync(item);
            await context.SaveChangesAsync();
            return Ok("Notícia salva com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar notícia");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<News>> Get([FromRoute] int id)
    {
        try
        {
            if (await context.NewsItems.AnyAsync(p => p.Id == id))
                return Ok(await context.NewsItems.Include(p => p.UserNews).FirstOrDefaultAsync(p => p.Id == id));
            else
                return NotFound("Notícia informada não encontrada");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca da notícia");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] News model)
    {
        if (id != model.Id)
            return BadRequest("Notícia inválida");

        try
        {
            if (!await context.NewsItems.AnyAsync(p => p.Id == id))
                return NotFound("Notícia inválida");

            context.NewsItems.Update(model);
            await context.SaveChangesAsync();
            return Ok("Notícia salva com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar notícia");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            News model = await context.NewsItems.FindAsync(id);

            if (model == null)
                return NotFound("Notícia inválida");

            context.NewsItems.Remove(model);
            await context.SaveChangesAsync();
            return Ok("Notícia removida com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover notícia");
        }
    }
}