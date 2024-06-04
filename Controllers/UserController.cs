using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
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
            return BadRequest("Erro ao listar os post");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] User item)
    {
        try
        {
            if(context.Users.Any(p => p.Nome == item.Nome))
                return BadRequest("Já existe tipo de curso com o nome informado");
                
            await context.Users.AddAsync(item);
            await context.SaveChangesAsync();
            return Ok("Tipo de curso salvo com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar o tipo de curso informado");
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
                return NotFound("O tipo de curso informado não foi encontrado");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca de tipo de curso");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] User model)
    {
        if (id != model.Id)
            return BadRequest("Tipo de curso inválido");

        try
        {
            if (!await context.Users.AnyAsync(p => p.Id == id))
                return NotFound("Tipo de curso inválido");

            context.Users.Update(model);
            await context.SaveChangesAsync();
            return Ok("Tipo de curso salvo com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar o tipo de curso informado");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            User model = await context.Users.FindAsync(id);

            if (model == null)
                return NotFound("Usuário inválido");

            context.Users.Remove(model);
            await context.SaveChangesAsync();
            return Ok("Usuário removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover o usuário");
        }
    }

    [HttpGet("pesquisaNome/{nome}")]
    public async Task<ActionResult<IEnumerable<User>>> Get([FromRoute] string nome)
    {
        try
        {
            List<User> resultado = await context.Users.Where(p => p.Nome == nome).ToListAsync();
            return Ok(resultado);
        }
        catch
        {
            return BadRequest("Falha ao buscar um usuário");
        }
    }

    [HttpGet("pesquisaNomeSemelhante/{nome}")]
    public async Task<ActionResult<IEnumerable<User>>> PesquisaNomeSemelhante([FromRoute] string nome)
    {
        try
        {
            List<User> resultado = await context.Users.
            Where(p => p.Nome.Contains(nome)).ToListAsync();
            return Ok(resultado);
        }
        catch
        {
            return BadRequest("Falha ao buscar um usuário");
        }
    }

    [Route("pesquisa")]
    [HttpPost]
    public async Task<ActionResult<IEnumerable<User>>> Pesquisa([FromBody] object item)
    {
        try
        {
            User model = JsonSerializer.Deserialize<User>(item.ToString());

            List<User> resultado = await context.Users
                .WhereIf(model.Nome != null, p => p.Nome == model.Nome)
                .WhereIf(model.Descricao != null, p => p.Descricao == model.Descricao).ToListAsync();

            return Ok(resultado);
        }
        catch
        {
            return BadRequest();
        }
    }
}