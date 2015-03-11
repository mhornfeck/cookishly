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
}