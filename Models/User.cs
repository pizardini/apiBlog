using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100, ErrorMessage = "O nome deve possuir, no máximo, 100 caracteres")]
    public string Nome { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "A descrição deve possuir, no mínimo, 5 caracteres")]
    [MaxLength(100)]

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }
    public string Password { get; set; }

    public string Code { get; set; }

    public UserRole Role {get; set; }
    public bool Active { get; set; }
}