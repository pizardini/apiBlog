using System.ComponentModel.DataAnnotations;

public class News
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O Título é obrigatório")]
    [MinLength(3)]
    [MaxLength(100, ErrorMessage = "O título deve possuir, no máximo, 100 caracteres")]
    public string Headline { get; set; }

    [Required]
    public string Subhead { get; set; }

    [Required]
    public string Text { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime PublicationDateTime { get; set; }

    [Required]
    public int UserId { get; set; }

    public User? UserNews { get; set; }
}