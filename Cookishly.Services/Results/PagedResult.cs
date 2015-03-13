using System.Collections.Generic;

namespace Cookishly.Services.Results
{
    public class PagedResult<T> : IPagedResult<T> where T : class
    {
        public int TotalItemCount { get; set; }
        public int PageItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IList<T> Items { get; set; }

        public PagedResult(IList<T> items, int totalItemCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageItemCount = items.Count;
        }
    }
}