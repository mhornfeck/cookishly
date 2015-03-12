using System.Collections.Generic;

namespace Cookishly.Services
{
    public class PagedResult<T> : ServiceResult, IPagedResult<T> where T : class
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IList<T> Data { get; set; }

        private PagedResult(bool isSuccess, string message, int totalRecords, int pageSize, 
            int pageNumber, IList<T> data) : base(isSuccess, message)
        {
            TotalRecords = totalRecords;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Data = data;
        }

        public static IPagedResult<T> Success(int totalRecords, int pageSize,
            int pageNumber, IList<T> data, string message = "")
        {
            return new PagedResult<T>(true, message, totalRecords, pageSize, pageNumber, data);
        }

        public new static IPagedResult<T> Fail(string message = "")
        {
            return new PagedResult<T>(false, message, 0, 0, 0, null);
        }
    }
}