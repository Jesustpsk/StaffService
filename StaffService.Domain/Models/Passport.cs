using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StaffService.Domain.Models;

public class Passport
{
    [JsonIgnore]
    [Column("id")]
    public int Id { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [Column("number")]
    public string Number { get; set; }
}