using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly DataContext context;

    public AuthorController(DataContext _context)
    {
        context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> Get()
    {
        try
        {
            return Ok(await context.Authors.ToListAsync());
        }
        catch
        {
            return BadRequest("Erro ao listar usuários");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Author item)
    {
        try
        {
            if(context.Authors.Any(p => p.Email == item.Email))
                return BadRequest("Já existe um usuário com este e-mail");
                
            await context.Authors.AddAsync(item);
            await context.SaveChangesAsync();
            return Ok("usuário salvo com sucesso");
        }
        catch
        {
            return BadRequest("Erro ao salvar o usuário");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> Get([FromRoute] int id)
    {
        try
        {
            if (await context.Authors.AnyAsync(p => p.Id == id))
                return Ok(await context.Authors.FindAsync(id));
            else
                return NotFound("O usuário informado não foi encontrado");
        }
        catch
        {
            return BadRequest("Erro ao efetuar a busca de usuário");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Author model)
    {
        if (id != model.Id)
            return BadRequest("Usuário inválido");

        try
        {
            if (!await context.Authors.AnyAsync(p => p.Id == id))
                return NotFound("Usuário não encontrado");

            context.Authors.Update(model);
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
            Author model = await context.Authors.FindAsync(id);

            if (model == null)
                return NotFound("Usuário inválido");

            context.Authors.Remove(model);
            await context.SaveChangesAsync();
            return Ok("Usuário removido com sucesso");
        }
        catch
        {
            return BadRequest("Falha ao remover o usuário");
        }
    }

    [HttpGet("pesquisaNome/{name}")]
    public async Task<ActionResult<IEnumerable<Author>>> Get([FromRoute] string name)
    {
        try
        {
            List<Author> resultado = await context.Authors.Where(p => p.Name == name).ToListAsync();
            return Ok(resultado);
        }
        catch
        {
            return BadRequest("Falha ao buscar um usuário");
        }
    }

    [HttpGet("pesquisaNomeSemelhante/{name}")]
    public async Task<ActionResult<IEnumerable<Author>>> PesquisaNomeSemelhante([FromRoute] string name)
    {
        try
        {
            List<Author> resultado = await context.Authors.
            Where(p => p.Name.Contains(name)).ToListAsync();
            return Ok(resultado);
        }
        catch
        {
            return BadRequest("Falha ao buscar um usuário");
        }
    }

    // [Route("pesquisa")]
    // [HttpPost]
    // public async Task<ActionResult<IEnumerable<Author>>> Pesquisa([FromBody] object item)
    // {
    //     try
    //     {
    //         Author model = JsonSerializer.Deserialize<Author>(item.ToString());

    //         List<Author> resultado = await context.Authors
    //             .WhereIf(model.Name != null, p => p.Name == model.Name)
    //             .WhereIf(model.Descricao != null, p => p.Descricao == model.Descricao).ToListAsync();

    //         return Ok(resultado);
    //     }
    //     catch
    //     {
    //         return BadRequest();
    //     }
    // }
}