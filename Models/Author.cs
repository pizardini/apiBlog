using System.ComponentModel.DataAnnotations;

public class Author
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100, ErrorMessage = "O nome deve possuir, no máximo, 100 caracteres")]
    public string Name { get; set; }

    public string Nickname { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // public ICollection<News> NewsItems { get; } = new List<News>();

    public DateTime BirthDate { get; set; }

    public string Password { get; set; }

    public string Code { get; set; }

    public bool Active { get; set; }
}