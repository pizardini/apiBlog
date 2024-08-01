using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Author : User
{
    public string Nickname { get; set; }

    public DateTime BirthDate { get; set; }

}