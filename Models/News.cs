using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class News
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O Título é obrigatório")]
    [MinLength(3)]
    [MaxLength(100, ErrorMessage = "O título deve possuir, no máximo, 100 caracteres")]
    public string Headline { get; set; }

    [Required(ErrorMessage = "O subtítulo é obrigatório")]
    public string Subhead { get; set; }

    [Required(ErrorMessage = "O texto é obrigatório")]
    public string Text { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime PublicationDateTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdate { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [NotMapped]
    public Author? AuthorNews { get; set; }

    [Required]
    public bool Published { get; set; }
}

