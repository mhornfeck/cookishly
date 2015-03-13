using System.Collections.Generic;

namespace Cookishly.Services.Results
{
    public interface IPagedResult<T>
    {
        int TotalItemCount { get; set; }
        int PageItemCount { get; set; }
        int PageSize { get; set; }
        int PageNumber { get; set; }

        IList<T> Items { get; set; }
    }
}