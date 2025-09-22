using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Passport
{
    [Column("id")]
    public int Id { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [Column("number")]
    public string Number { get; set; }
}