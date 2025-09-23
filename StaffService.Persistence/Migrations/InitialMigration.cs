using System.Data;
using FluentMigrator;
using FluentMigrator.Postgres;

namespace StaffService.Persistence.Migrations;

[Migration(20250922155259)]
public class InitialMigration_20250922155259 : Migration
{
    public override void Up()
    {
        Create.Table("passports").WithDescription("Паспорта")
            .WithColumn("id")
            .AsInt32()
            .PrimaryKey()
            .Identity(PostgresGenerationType.Always)
            .WithColumn("type").AsString(50)
            .WithColumn("number").AsString(20);
        
        Create.Table("departments").WithDescription("Департаменты")
            .WithColumn("id")
            .AsInt32()
            .PrimaryKey()
            .Identity(PostgresGenerationType.Always)
            .WithColumn("name").AsString(100)
            .WithColumn("phone").AsString(20);
        
        Create.Table("employees").WithDescription("Список сотрудников")
            .WithColumn("id").AsInt32()
                .PrimaryKey()
                    .Identity(PostgresGenerationType.Always)
            .WithColumn("name").AsString(50)
            .WithColumn("surname").AsString(50)
            .WithColumn("phone").AsString(20)
            .WithColumn("company_id").AsInt32()
                .NotNullable()
            .WithColumn("passport_id").AsInt32()
                .ForeignKey("fk_passport","passports", "id")
                    .OnDeleteOrUpdate(Rule.Cascade)
            .WithColumn("department_id").AsInt32()
                .ForeignKey("fk_department", "departments", "id")
                    .OnDeleteOrUpdate(Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table("employees");
        Delete.Table("departments");
        Delete.Table("passports");
    }
}