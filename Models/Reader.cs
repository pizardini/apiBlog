using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Reader
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(100, ErrorMessage = "O nome deve possuir, no máximo, 100 caracteres")]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public DateTime BirthDate { get; set; }
    public string Password { get; set; }

    [NotMapped]
    public string Token { get; set; }

    public bool Active { get; set; }
}