using Entity;

namespace DAL.Extensions
{
    public static class JsonQueryExtensions
    {
        // [Methods - JSON Query Extensions]
        
        /// <summary>
        /// Filters requests by JSON property value using string contains
        /// </summary>
        public static IQueryable<Requests> WhereJsonProperty(this IQueryable<Requests> query, string propertyPath, string value)
        {
            return query.Where(r => r.data != null && r.data.Contains($"\"{propertyPath}\":\"{value}\""));
        }

        /// <summary>
        /// Filters requests by JSON property value using LIKE operator
        /// </summary>
        public static IQueryable<Requests> WhereJsonPropertyContains(this IQueryable<Requests> query, string propertyPath, string value)
        {
            return query.Where(r => r.data != null && r.data.Contains($"\"{propertyPath}\"") && r.data.Contains(value));
        }

        /// <summary>
        /// Validates JSON data format by checking basic structure
        /// </summary>
        public static IQueryable<Requests> WhereValidJson(this IQueryable<Requests> query)
        {
            return query.Where(r => r.data != null && r.data.StartsWith("{") && r.data.EndsWith("}"));
        }

        /// <summary>
        /// Basic ordering by creation date (since JSON ordering is complex without SQL Server JSON functions)
        /// </summary>
        public static IQueryable<Requests> OrderByCreationDate(this IQueryable<Requests> query, bool descending = false)
        {
            return descending 
                ? query.OrderByDescending(r => r.createdAt)
                : query.OrderBy(r => r.createdAt);
        }
    }
}