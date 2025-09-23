using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StaffService.Domain.Models;

public class Department
{
    [JsonIgnore]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
}