using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Extensions
{
    public static class JsonQueryExtensions
    {
        // [Methods - JSON Query Extensions using SQL Server native functions]
        
        /// <summary>
        /// Filters requests by JSON property value using JSON_VALUE function
        /// </summary>
        public static IQueryable<Requests> WhereJsonProperty(this IQueryable<Requests> query, string propertyPath, string value)
        {
            return query.Where(r => r.data != null && 
                EF.Functions.Like(
                    EF.Property<string>(r, $"JSON_VALUE([data], '$.{propertyPath}')"), 
                    value
                ));
        }

        /// <summary>
        /// Filters requests by JSON property value using JSON_VALUE with LIKE operator
        /// </summary>
        public static IQueryable<Requests> WhereJsonPropertyContains(this IQueryable<Requests> query, string propertyPath, string value)
        {
            return query.Where(r => r.data != null && 
                EF.Functions.Like(
                    EF.Property<string>(r, $"JSON_VALUE([data], '$.{propertyPath}')"), 
                    $"%{value}%"
                ));
        }

        /// <summary>
        /// Validates JSON data format using ISJSON function
        /// </summary>
        public static IQueryable<Requests> WhereValidJson(this IQueryable<Requests> query)
        {
            return query.Where(r => r.data != null && 
                EF.Property<bool>(r, "ISJSON([data])"));
        }

        /// <summary>
        /// Filters by JSON property existence using JSON_QUERY
        /// </summary>
        public static IQueryable<Requests> WhereJsonPropertyExists(this IQueryable<Requests> query, string propertyPath)
        {
            return query.Where(r => r.data != null && 
                EF.Property<string>(r, $"JSON_QUERY([data], '$.{propertyPath}')") != null);
        }

        /// <summary>
        /// Filters by JSON property numeric value using JSON_VALUE with CAST
        /// </summary>
        public static IQueryable<Requests> WhereJsonNumericProperty(this IQueryable<Requests> query, string propertyPath, decimal value, string operation = "=")
        {
            var sqlOperation = operation switch
            {
                ">" => ">",
                "<" => "<",
                ">=" => ">=",
                "<=" => "<=",
                "!=" => "!=",
                _ => "="
            };

            return query.Where(r => r.data != null && 
                EF.Property<bool>(r, $"CAST(JSON_VALUE([data], '$.{propertyPath}') AS DECIMAL(18,2)) {sqlOperation} {value}"));
        }

        /// <summary>
        /// Filters by JSON property date value using JSON_VALUE with CAST
        /// </summary>
        public static IQueryable<Requests> WhereJsonDateProperty(this IQueryable<Requests> query, string propertyPath, DateTime value, string operation = "=")
        {
            var sqlOperation = operation switch
            {
                ">" => ">",
                "<" => "<",
                ">=" => ">=",
                "<=" => "<=",
                "!=" => "!=",
                _ => "="
            };

            var dateString = value.ToString("yyyy-MM-dd HH:mm:ss");
            return query.Where(r => r.data != null && 
                EF.Property<bool>(r, $"CAST(JSON_VALUE([data], '$.{propertyPath}') AS DATETIME2) {sqlOperation} '{dateString}'"));
        }

        /// <summary>
        /// Orders by JSON property value using JSON_VALUE
        /// </summary>
        public static IQueryable<Requests> OrderByJsonProperty(this IQueryable<Requests> query, string propertyPath, bool descending = false)
        {
            return descending 
                ? query.OrderByDescending(r => EF.Property<string>(r, $"JSON_VALUE([data], '$.{propertyPath}')"))
                : query.OrderBy(r => EF.Property<string>(r, $"JSON_VALUE([data], '$.{propertyPath}')"));
        }

        /// <summary>
        /// Searches in multiple JSON properties using JSON_VALUE
        /// </summary>
        public static IQueryable<Requests> WhereJsonMultipleProperties(this IQueryable<Requests> query, Dictionary<string, string> propertyValues)
        {
            foreach (var kvp in propertyValues)
            {
                query = query.Where(r => r.data != null && 
                    EF.Functions.Like(
                        EF.Property<string>(r, $"JSON_VALUE([data], '$.{kvp.Key}')"), 
                        kvp.Value
                    ));
            }
            return query;
        }

        /// <summary>
        /// Uses OPENJSON to extract and filter by array elements
        /// </summary>
        public static IQueryable<Requests> WhereJsonArrayContains(this IQueryable<Requests> query, string arrayPath, string value)
        {
            return query.Where(r => r.data != null && 
                EF.Property<bool>(r, $"EXISTS (SELECT 1 FROM OPENJSON([data], '$.{arrayPath}') WHERE [value] = '{value}')"));
        }

        /// <summary>
        /// Basic ordering by creation date
        /// </summary>
        public static IQueryable<Requests> OrderByCreationDate(this IQueryable<Requests> query, bool descending = false)
        {
            return descending 
                ? query.OrderByDescending(r => r.createdAt)
                : query.OrderBy(r => r.createdAt);
        }
    }
}