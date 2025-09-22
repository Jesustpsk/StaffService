using System.Data;
using FluentMigrator;
using FluentMigrator.Postgres;

namespace Persistence.Migrations;

[Migration(20250922155259)]
public class InitialMigration_20250922155259 : Migration
{
    public override void Up()
    {
        Create.Table("passports").WithDescription("Паспорта")
            .WithColumn("id")
            .AsInt64()
            .PrimaryKey()
            .Identity(PostgresGenerationType.Always)
            .WithColumn("type").AsString(50)
            .WithColumn("number").AsString(20);
        
        Create.Table("departments").WithDescription("Департаменты")
            .WithColumn("id")
            .AsInt64()
            .PrimaryKey()
            .Identity(PostgresGenerationType.Always)
            .WithColumn("name").AsString(100)
            .WithColumn("phone").AsString(20);
        
        Create.Table("employees").WithDescription("Список сотрудников")
            .WithColumn("id").AsInt64()
                .PrimaryKey()
                    .Identity(PostgresGenerationType.Always)
            .WithColumn("name").AsString(50)
            .WithColumn("surname").AsString(50)
            .WithColumn("phone").AsString(20)
            .WithColumn("company_id").AsInt64()
                .NotNullable()
            .WithColumn("passport_id").AsInt64()
                .ForeignKey("fk_passport","passports", "id")
                    .OnDelete(Rule.Cascade)
            .WithColumn("department_id").AsInt64()
                .ForeignKey("fk_department", "departments", "id")
                    .OnDelete(Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table("employees");
        Delete.Table("departments");
        Delete.Table("passports");
    }
}