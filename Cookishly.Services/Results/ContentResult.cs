namespace Cookishly.Services.Results
{
    public class ContentResult<T> : ServiceResult, IResult<T> where T : class
    {
        public T Content { get; set; }

        public static IResult<T> Success(T content, string message = "")
        {
            return new ContentResult<T>
            {
                Content = content,
                Message = message,
                IsSuccess = true
            };
        }

        public new static IResult<T> Fail(string message = "", FailureType? failureType = Results.FailureType.Other)
        {
            return new ContentResult<T>
            {
                FailureType = failureType,
                Message = message,
                IsSuccess = false
            };
        }
    }
}