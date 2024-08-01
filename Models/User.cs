using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class User
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
    
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool Active { get; set; }

    public int Type {get; set; }
}