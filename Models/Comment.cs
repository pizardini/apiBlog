using System.ComponentModel.DataAnnotations;

public class Comment
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O texto é obrigatório")]
    [MinLength(3, ErrorMessage = "O comentário deve possuir no mínimo 3 caracteres")]
    [MaxLength(1000, ErrorMessage = "O comentário deve possuir, no máximo, 1000 caracteres")]
    public string Content { get; set; }

    [Required]
    public int ReaderId {get; set; }
    public Reader ReaderComment {get; set; }
    
    [Required]
    public DateTime DatePublished {get; set; }
 }