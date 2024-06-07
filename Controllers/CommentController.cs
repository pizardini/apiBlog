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
public class CommentController : ControllerBase
{
    private readonly DataContext context;

    public CommentController(DataContext _context)
    {
        context = _context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> Get()
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
}