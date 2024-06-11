using System;
using System.ComponentModel.DataAnnotations;

public class Reaction
{
    public int Id { get; set; }
    public int Type { get; set; }

    [Required]
    public int NewsId { get; set; }
    public News? NewsReaction {get; set;}

    [Required]
    public int ReaderId {get; set; }
    public Reader? ReaderReaction {get; set; }
}