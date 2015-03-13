using System.Security.Cryptography.X509Certificates;

namespace Cookishly.Services.Results
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
        FailureType? FailureType { get; set; }
    }

    public interface IResult<T> : IResult where T : class
    {
        T Content { get; set; }
    }
}