using System.Collections;
using System.Collections.Generic;

namespace Cookishly.Services
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }

    public interface IResult<T> : IResult where T : class
    {
        T Payload { get; set; }
    }

    public interface IPagedResult<T> : IResult where T : class
    {
        int TotalRecords { get; set; }
        int PageSize { get; set; }
        int PageNumber { get; set; }

        IList<T> Data { get; set; }
    }
}