using Dapper;

namespace StaffService.Persistence.Helpers;

public static class SqlFilterHelper
{
    public static void ApplyFilters<TQuery>(
        SqlBuilder builder,
        TQuery query)
    {
        var props = typeof(TQuery).GetProperties();

        foreach (var prop in props)
        {
            var value = prop.GetValue(query);
            if (value == null) continue;

            if (prop.Name is "SortBy" or "Ascending" or "Page" or "PageSize")
                continue;

            builder.Where($"{prop.Name} = @{prop.Name}");
        }
    }
}

