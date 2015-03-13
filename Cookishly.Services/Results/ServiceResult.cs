namespace Cookishly.Services.Results
{
    public class ServiceResult : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public FailureType? FailureType { get; set; }

        public static IResult Success(string message = "")
        {
            return new ServiceResult
            {
                Message = message,
                IsSuccess = true
            };
        }

        public static IResult Fail(string message = "", FailureType? failureType = Results.FailureType.Other)
        {
            return new ServiceResult
            {
                Message = message,
                FailureType = failureType,
                IsSuccess = false
            };
        }
    }
}