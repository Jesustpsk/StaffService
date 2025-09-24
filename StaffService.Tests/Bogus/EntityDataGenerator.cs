using System.Reflection;
using Bogus;

namespace StaffService.Tests.Bogus;

public class EntityDataGenerator<TEntity> where TEntity : class, new()
{
    private readonly Faker<TEntity> _faker;
    
    /// <summary>
    /// Генератор сущностей через рефлексию и богус.
    /// </summary>
    /// <param name="entityIsNull">True - генерирует сущность, свойства которой все равны null, False (default) - генерирует сущность с заполненными свойствами</param>
    public EntityDataGenerator(bool entityIsNull = false)
    {
        _faker = new Faker<TEntity>()
            .FinishWith((f, entity) =>
            {
                foreach (var prop in typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!prop.CanWrite) continue;
    
                    var type = prop.PropertyType;

                    if (type.IsClass && type != typeof(string))
                        continue;
                        
                    var value = entityIsNull 
                        ? null 
                        : GenerateValueForProperty(f, prop);
                    prop.SetValue(entity, value);
                }
            });
    }

    public TEntity Generate() => _faker.Generate();

    private static object GenerateValueForProperty(Faker f, PropertyInfo prop)
    {
        var name = prop.Name.ToLowerInvariant();
        var type = prop.PropertyType;

        if (type == typeof(string))
        {
            if (name.Contains("name")) return f.Name.FirstName();
            if (name.Contains("surname") || name.Contains("lastname")) return f.Name.LastName();
            if (name.Contains("phone")) return f.Phone.PhoneNumber();
            if (name.Contains("email")) return f.Internet.Email();
            if (name.Contains("type")) return f.Commerce.Product();
            if (name.Contains("number")) return f.Random.AlphaNumeric(10);

            return f.Lorem.Word();
        }

        if (type == typeof(int) || type == typeof(int?)) return f.Random.Int(1, 1000);
        if (type == typeof(decimal) || type == typeof(decimal?)) return f.Finance.Amount();
        if (type == typeof(DateTime) || type == typeof(DateTime?)) return f.Date.Past();

        if (type.IsClass && type != typeof(string))
        {
            // рекурсивная генерация вложенной сущности
            var generatorType = typeof(EntityDataGenerator<>).MakeGenericType(type);
            var generator = Activator.CreateInstance(generatorType);
            var method = generatorType.GetMethod("Generate");
            return method!.Invoke(generator, null);
        }

        return null!;
    }
}