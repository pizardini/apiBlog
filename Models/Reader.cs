using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Reader : User
{
    public DateTime BirthDate { get; set; }

}