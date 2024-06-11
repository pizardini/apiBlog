using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    // [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
    [NotMapped]
    public string Token { get; set; }

    [Required]
    public bool Active { get; set; }
}