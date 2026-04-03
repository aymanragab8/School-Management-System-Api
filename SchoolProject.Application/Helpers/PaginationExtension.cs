using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Helpers
{
    public static class PaginationExtension
    {
        public static async Task<PaginatedResponse<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> source, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalCount = await source.CountAsync();

            var data = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}