using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Employee
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("surname")]
    public string Surname { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
    [Column("company_id")]
    public int CompanyId { get; set; }
    [Column("passport_id")]
    public int PassportId { get; set; }
    public Passport Passport { get; set; }
    [Column("department_id")]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}