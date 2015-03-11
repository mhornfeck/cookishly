namespace Cookishly.Services
{
    public class ServiceResult : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        private ServiceResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static IResult Success(string message = "")
        {
            return new ServiceResult(true, message);
        }

        public static IResult Fail(string message = "")
        {
            return new ServiceResult(false, message);
        }
    }

    public class ServiceResult<T> : IResult<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }

        private ServiceResult(bool isSuccess, string message, T payload)
        {
            IsSuccess = isSuccess;
            Message = message;
            Payload = payload;
        }

        public static IResult<T> Success(T payload, string message = "")
        {
            return new ServiceResult<T>(true, message, payload);
        }

        public static IResult<T> Fail(string message = "")
        {
            return new ServiceResult<T>(false, message, null);
        }
    }
}