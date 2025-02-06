namespace Barber.Colocho.Core.Helpers
{
    public static class QueryableExtensions
    {
        public static IEnumerable<T> Pagination<T>(this IEnumerable<T> queryable, int page, int countRegisterPage)
        {
            return queryable
                .Skip((page - 1) * countRegisterPage)
                .Take(countRegisterPage);
        }
    }
}
