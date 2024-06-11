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
}