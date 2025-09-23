using System.ComponentModel.DataAnnotations.Schema;

namespace StaffService.Domain.Models;

public class Company
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("address")]
    public string Address { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
    [Column("email")]
    public string Email { get; set; }
}